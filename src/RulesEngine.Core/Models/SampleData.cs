using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RulesEngine.Models
{
    public static class SampleData
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<PolicyContext>())
            {
                if (await db.Database.EnsureCreatedAsync())
                {
                    var models = new PolicyModel[]
                    {
                        new PolicyModel { Name = "JToken", TypeName = typeof(JToken).FullName, Policies = new Policy[] { } }
                    };
                    db.PolicyModels.AddRange(models);

                    await db.SaveChangesAsync();
                }
            }
        }

    }
}
