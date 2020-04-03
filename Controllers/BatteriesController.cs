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
    public class BatteryController : ControllerBase
    {
        private readonly TodoContext _context;
        public BatteryController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> Getbatteries()
        {
            return await _context.batteries.ToListAsync();
        }

        // GET: api/batteries/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> Getbatteries(long id)
        {
            var batteries = await _context.batteries.FindAsync(id);
            if (batteries == null)
            {
                return NotFound();
            }
            return batteries;
        }

        // PUT: api/batteries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putbatteries(long id, Battery batteries)
        {
            if (id != batteries.id)
            {
                return BadRequest();
            }
            _context.Entry(batteries).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!batteriesExists(id))
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
        
        // POST: api/batteries
        [HttpPost]
        public async Task<ActionResult<Battery>> Postbatteries(Battery batteries)
        {
            _context.batteries.Add(batteries);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("Getbatteries", new { id = batteries.Id }, batteries);
            return CreatedAtAction(nameof(Getbatteries), new { id = batteries.id }, batteries);
        }

        // DELETE: api/batteries/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Battery>> Deletebatteries(long id)
        {
            var batteries = await _context.batteries.FindAsync(id);
            if (batteries == null)
            {
                return NotFound();
            }
            _context.batteries.Remove(batteries);
            await _context.SaveChangesAsync();
            return batteries;
        }
        private bool batteriesExists(long id)
        {
            return _context.batteries.Any(e => e.id == id);
        }
    }
}