using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RulesEngine.Core;
using RulesEngine.Models;

namespace RulesEngine.Controllers
{
    [Route("api/[controller]")]
    public class JsonObjectController : GenericModelController<JToken, JToken>
    {
        public JsonObjectController(ILogger<GenericModelController<JToken, JToken>> logger, PolicyContext context) : base (logger, context)
        {

        }

        [HttpGet("[action]")]
        public IActionResult Test()
        {
            var sw = new Stopwatch();
            sw.Start();


            var policyVersion = new PolicyVersion { PolicyRules = new List<PolicyRule> { new PolicyRule { Condition = "true", Value = "Root" } } };
            try
            {
                var core = new CSharpScriptCore<JToken>();
                var  result = core.RunPolicy<JToken>(policyVersion, JToken.FromObject(policyVersion));                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                //Log.Error(ex);
                return BadRequest(ex);
            }
            finally
            {
                sw.Stop();
                _logger.LogError($"RunPolicy Name: {policyVersion.Id}, Version: {policyVersion.Id}, Elapsed: {sw.ElapsedTicks}");
            }
        }

    }
}
