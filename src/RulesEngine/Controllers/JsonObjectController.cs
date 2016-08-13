using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RulesEngine.Core;
using RulesEngine.Models;

namespace RulesEngine.Controllers
{
    [Route("api/[controller]")]
    public class JsonObjectController : GenericModelController<JObject, string>
    {
        [HttpGet("[action]")]
        public IActionResult Test()
        {
            var policyVersion = new PolicyVersion { PolicyRules = new List<PolicyRule> { new PolicyRule { Condition = "true", Value = "ToString()" }} };
            string result = string.Empty;
            try
            {
                var core = new CSharpScriptCore<JObject>();
                result = core.RunPolicy<string>(policyVersion, JObject.FromObject(policyVersion));
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(ex);
            }
        }

    }
}
