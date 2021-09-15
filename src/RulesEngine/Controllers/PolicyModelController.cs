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
    public class PolicyModelController : Controller
    {
        private PolicyContext db;

        public PolicyModelController (PolicyContext context)
        {
            db = context;
        }

        [Route("api/model")]
        [HttpGet]
        public async Task<List<PolicyModel>> GetModels()
        {            
            return await db.PolicyModels.ToListAsync();
        }

        [Route("api/model/{name}", Name = "GetModel")]
        [HttpGet]
        public async Task<IActionResult> GetModel(string name)
        {
            var model = await db.PolicyModels.SingleOrDefaultAsync(m => m.Name == name);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [Route("api/model")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PolicyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.PolicyModels.Add(model);
            await db.SaveChangesAsync();
            return CreatedAtRoute("GetModel", new { name = model.Name }, model);
        }

        [Route("api/model/{id}")]
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]PolicyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        [Route("api/model/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await db.PolicyModels.SingleOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            db.PolicyModels.Remove(model);
            await db.SaveChangesAsync();
            return Ok(model);
        }

        private bool ModelExists(int id)
        {
            return db.PolicyModels.Count(e => e.Id == id) > 0;
        }
    }
}

