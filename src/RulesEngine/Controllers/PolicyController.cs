using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RulesEngine.Core;
using RulesEngine.Models;

namespace RulesEngine.Controllers
{
    public class PolicyController : Controller
    {
        private PolicyContext db;

        public PolicyController(PolicyContext context)
        {
            db = context;          
        }

        [Route("api/model/{model}/policy")]
        [HttpGet]
        public async Task<List<Policy>> GetPolicies(string model)
        {            
            return await db.Policies.AsNoTracking().Where(p => p.PolicyModel.Name == model).ToListAsync();
        }

        [Route("api/model/{model}/policy/{name}", Name = "GetPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetPolicy(string name)
        {
            var policy = await db.Policies.AsNoTracking().SingleOrDefaultAsync(p => p.Name == name);
            if (policy == null)
            {
                return NotFound();
            }
            return Ok(policy);
        }

        [Route("api/model/{model}/policy")]
        [HttpPost]
        public async Task<IActionResult> PostPolicy([FromBody]Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            policy.PolicyVersions.Add(new PolicyVersion());
            db.Policies.Add(policy);
            await db.SaveChangesAsync();
            return CreatedAtRoute("GetPolicy", new { name = policy.Name }, policy);
        }

        [Route("api/model/{model}/policy/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var policy = await db.Policies.SingleOrDefaultAsync(p => p.Id == id);
            if (policy == null)
            {
                return NotFound();
            }
            db.Policies.Remove(policy);
            await db.SaveChangesAsync();
            return Ok(policy);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

