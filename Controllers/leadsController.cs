using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly TodoContext _context;
        public LeadsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leads>>> Getleads()
        {
            return await _context.leads.ToListAsync();
        }

        // GET: api/leads/available
        [HttpGet("notacustomer")]
        public List<Leads> Getnotacustomer(string status)
        {
            var notacustomer = _context.leads.Where(l => l.customers.Any(le => le.compagny_email)).ToList();
            return notacustomer;
        }

        // PUT: api/leads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putleads(long id, Leads leads)
        {
            if (id != leads.id)
            {
                return BadRequest();
            }
            _context.Entry(leads).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!leadsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        
        // POST: api/leads
        [HttpPost]
        public async Task<ActionResult<Leads>> Postleads(Leads leads)
        {
            _context.leads.Add(leads);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("Getleads", new { id = leads.Id }, leads);
            return CreatedAtAction(nameof(Getleads), new { id = leads.id }, leads);
        }

        // DELETE: api/leads/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Leads>> Deleteleads(long id)
        {
            var leads = await _context.leads.FindAsync(id);
            if (leads == null)
            {
                return NotFound();
            }
            _context.leads.Remove(leads);
            await _context.SaveChangesAsync();
            return leads;
        }
        private bool leadsExists(long id)
        {
            return _context.leads.Any(e => e.id == id);
        }
    }
}