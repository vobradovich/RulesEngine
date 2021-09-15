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
    public class PolicyVersionController : Controller
    {
        private PolicyContext db;

        public PolicyVersionController(PolicyContext context)
        {
            db = context;
        }

        [Route("api/model/{model}/policy/{policy}/version/{id}", Name = "GetVersion")]
        [HttpGet]
        public async Task<IActionResult> GetVersion(int id)
        {
            var policy = await db.PolicyVersions.AsNoTracking().SingleOrDefaultAsync(v => v.Id == id);
            if (policy == null)
            {
                return NotFound();
            }
            return Ok(policy);
        }

        [Route("api/model/{model}/policy/{policy}/version")]
        [HttpPost]
        public async Task<IActionResult> PostVersion([FromBody]PolicyVersion policyVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            policyVersion.Id = 0;
            policyVersion.PolicyRules.ToList().ForEach(r => r.PolicyVersionId = 0);
            db.PolicyVersions.Add(policyVersion);
            await db.SaveChangesAsync();
            return CreatedAtRoute("GetVersion", new { id = policyVersion.Id }, policyVersion);
        }

        [Route("api/model/{model}/policy/{policy}/version/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteVersion(int id)
        {
            var policy = await db.PolicyVersions.SingleOrDefaultAsync(v => v.Id == id);
            if (policy == null)
            {
                return NotFound();
            }

            db.PolicyVersions.Remove(policy);
            await db.SaveChangesAsync();
            return Ok(policy);
        }
    }
}

