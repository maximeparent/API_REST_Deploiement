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
    public class customersController : ControllerBase
    {
        private readonly TodoContext _context;
        public customersController(TodoContext context)
        {
            _context = context;
        }
        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> Getcustomers()
        {
            return await _context.customers.ToListAsync();
        }

        // GET: api/customers/unavailable
        [HttpGet("unavailable")]
        public List<Customers> Getstatus(string status)
        {
            var unavailable = _context.customers.Where(e => e.status != "Active").ToList();
            return unavailable;
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> Getcustomers(long id)
        {
            var customers = await _context.customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            return customers;
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcustomers(long id, Customers customers)
        {
            if (id != customers.id)
            {
                return BadRequest();
            }
            _context.Entry(customers).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customersExists(id))
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
        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<Customers>> Postcustomers(Customers customers)
        {
            _context.customers.Add(customers);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("Getcustomers", new { id = customers.Id }, customers);
            return CreatedAtAction(nameof(Getcustomers), new { id = customers.id }, customers);
        }

        // DELETE: api/customers/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customers>> Deletecustomers(long id)
        {
            var customers = await _context.customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            _context.customers.Remove(customers);
            await _context.SaveChangesAsync();
            return customers;
        }
        private bool customersExists(long id)
        {
            return _context.customers.Any(e => e.id == id);
        }
    }
}