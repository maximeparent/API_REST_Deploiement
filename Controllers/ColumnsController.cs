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
    public class columnsController : ControllerBase
    {
        private readonly TodoContext _context;
        public columnsController(TodoContext context)
        {
            _context = context;
        }
        // GET: api/columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Columns>>> Gecolumns()
        {
            return await _context.columns.ToListAsync();
        }
        // GET: api/columns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Columns>> Getcolumns(long id)
        {
            var columns = await _context.columns.FindAsync(id);
            if (columns == null)
            {
                return NotFound();
            }
            return columns;
        }
        // PUT: api/columns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcolumns(long id, Columns columns)
        {
            if (id != columns.id)
            {
                return BadRequest();
            }
            _context.Entry(columns).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!columnsExists(id))
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
        // POST: api/columns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        // POST: api/columns
        [HttpPost]
        public async Task<ActionResult<Columns>> Postcolumns(Columns columns)
        {
            _context.columns.Add(columns);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("Getcolumns", new { id = columns.Id }, columns);
            return CreatedAtAction(nameof(Getcolumns), new { id = columns.id }, columns);
        }
        // DELETE: api/columns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Columns>> Deletecolumns(long id)
        {
            var columns = await _context.columns.FindAsync(id);
            if (columns == null)
            {
                return NotFound();
            }
            _context.columns.Remove(columns);
            await _context.SaveChangesAsync();
            return columns;
        }
        private bool columnsExists(long id)
        {
            return _context.columns.Any(e => e.id == id);
        }
    }
}