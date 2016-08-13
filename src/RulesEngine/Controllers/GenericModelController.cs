using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RulesEngine.Core;
using RulesEngine.Models;

namespace RulesEngine.Controllers
{
    public abstract class GenericModelController<T, TResult> : Controller
    {
        private CSharpScriptCore<T> _core = new CSharpScriptCore<T>();       

        [HttpPost("[action]")]
        public virtual IActionResult ValidateRule([FromBody]string condition)
        {
            try
            {                
                _core.Validate(condition);
                return Ok();
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(ex);
            }
        }
    
        [HttpPost("[action]")]
        public virtual IActionResult Run(string policyName, [FromBody]T parameter)
        {
            var sw = new Stopwatch();
            sw.Start();

            // var policy = await db.Policies.SingleOrDefaultAsync(p => p.Name == policyName);
            // if (policy == null)
            // {
            //     return NotFound();
            // }
            // var policyVersion = policy.PolicyVersions.LastOrDefault();
            // if (policyVersion == null)
            // {
            //     return NotFound();
            // }
            var policyVersion = new PolicyVersion { PolicyRules = new List<PolicyRule> { new PolicyRule { Condition = "true", Value = "this.ToString()" }} };
            try
            {
                var val = _core.RunPolicy<TResult>(policyVersion, parameter);
                return Ok(val);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return BadRequest(ex);
            }
            finally
            {
                sw.Stop();
                // db.PolicyLogs.Add(new PolicyLog
                // {
                //     PolicyVersionId = policyVersion.Id,
                //     Model = JsonConvert.SerializeObject(parameter),
                //     Value = result,
                //     Duration = sw.Elapsed
                // });
                // await db.SaveChangesAsync();
            }
        }

        // public virtual async Task<IHttpActionResult> RunVersion(string modelName, string policyName, int id, [FromBody]T parameter)
        // {
        //     var policy = await db.Policies.SingleOrDefaultAsync(p => p.Name == policyName);
        //     if (policy == null)
        //     {
        //         return NotFound();
        //     }
        //     var policyVersion = await db.PolicyVersions.FindAsync(id);
        //     if (policyVersion == null)
        //     {
        //         return NotFound();
        //     }
        //     try
        //     {
        //         var core = policyVersion.ParseValue ? new CSharpScriptCore<T>() as IPolicyCore<T> : new DynamicExpressoCore<T>();
        //         var result = core.RunPolicy(modelName, policyVersion, parameter);
        //         return Ok(result);
        //     }
        //     catch (Exception ex)
        //     {
        //         Log.Error(ex, ex.Message);
        //         return BadRequest(ex.Message);
        //     }
        // }

        // protected override void Dispose(bool disposing)
        // {
        //     if (disposing)
        //     {
        //         db.Dispose();
        //     }
        //     base.Dispose(disposing);
        // }
    }
}

