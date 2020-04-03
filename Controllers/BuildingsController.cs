using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class buildingsController : ControllerBase
    {
        private readonly TodoContext _context;
        public buildingsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> Getbuildings()
        {
            return await _context.buildings.ToListAsync();
        }

        // GET: api/buildings/unavailable
        [HttpGet("intervention")]
        public List<Buildings> Getintervention(string status)
        {
            var myintervention = _context.buildings.Where(b => b.batteries.Any(ba => ba.status == "Inactive")).ToList();
            return myintervention;
        }

        // GET: api/buildings/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Buildings>> Getbuildings(long id)
        {
            var buildings = await _context.buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }
            return buildings;
        }

        // PUT: api/buildings/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Putbuildings(long id, Buildings buildings)
        {
            if (id != buildings.id)
            {
                return BadRequest();
            }
            _context.Entry(buildings).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!buildingsExists(id))
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
        
        // POST: api/buildings
        [HttpPost]
        public async Task<ActionResult<Buildings>> Postbuildings(Buildings buildings)
        {
            _context.buildings.Add(buildings);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Getbuildings), new { id = buildings.id }, buildings);
        }

        // DELETE: api/buildings/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Buildings>> Deletebuildings(long id)
        {
            var buildings = await _context.buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }
            _context.buildings.Remove(buildings);
            await _context.SaveChangesAsync();
            return buildings;
        }
        private bool buildingsExists(long id)
        {
            return _context.buildings.Any(e => e.id == id);
        }
    }
}