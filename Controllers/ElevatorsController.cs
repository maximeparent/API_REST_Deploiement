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
    public class elevatorsController : ControllerBase
    {
        private readonly TodoContext _context;
        public elevatorsController(TodoContext context)
        {
            _context = context;
        }
        // GET: api/elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevators>>> Getelevators()
        {
            return await _context.elevators.ToListAsync();
        }

        // GET: api/elevators/unavailable
        [HttpGet("unavailable")]
        public List<Elevators> Getstatus(string status)
        {
            var unavailable = _context.elevators.Where(e => e.status != "Active").ToList();
            return unavailable;
        }

        // GET: api/elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevators>> Getelevators(long id)
        {
            var elevators = await _context.elevators.FindAsync(id);
            if (elevators == null)
            {
                return NotFound();
            }
            return elevators;
        }

        // PUT: api/elevators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putelevators(long id, Elevators elevators)
        {
            if (id != elevators.id)
            {
                return BadRequest();
            }
            _context.Entry(elevators).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!elevatorsExists(id))
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
        // POST: api/elevators
        [HttpPost]
        public async Task<ActionResult<Elevators>> Postelevators(Elevators elevators)
        {
            _context.elevators.Add(elevators);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("Getelevators", new { id = elevators.Id }, elevators);
            return CreatedAtAction(nameof(Getelevators), new { id = elevators.id }, elevators);
        }

        // DELETE: api/elevators/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Elevators>> Deleteelevators(long id)
        {
            var elevators = await _context.elevators.FindAsync(id);
            if (elevators == null)
            {
                return NotFound();
            }
            _context.elevators.Remove(elevators);
            await _context.SaveChangesAsync();
            return elevators;
        }
        private bool elevatorsExists(long id)
        {
            return _context.elevators.Any(e => e.id == id);
        }
    }
}