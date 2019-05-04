using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testmastergroup.Models;

namespace testmastergroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class credencialesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public credencialesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/credenciales
        [HttpGet]
        public IEnumerable<credenciales> Getcredenciales()
        {
            return _context.credenciales;
        }

        // GET: api/credenciales/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getcredenciales([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var credenciales = await _context.credenciales.FindAsync(id);
            
            if (credenciales == null)
            {
                return NotFound();
            }

            return Ok(credenciales);
        }

        // PUT: api/credenciales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcredenciales([FromRoute] int id, [FromBody] credenciales credenciales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != credenciales.Id)
            {
                return BadRequest();
            }

            _context.Entry(credenciales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!credencialesExists(id))
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

        // POST: api/credenciales
        [HttpPost]
        public async Task<IActionResult> Postcredenciales([FromBody] credenciales credenciales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.credenciales.Add(credenciales);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcredenciales", new { id = credenciales.Id }, credenciales);
        }

        // DELETE: api/credenciales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecredenciales([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var credenciales = await _context.credenciales.FindAsync(id);
            if (credenciales == null)
            {
                return NotFound();
            }

            _context.credenciales.Remove(credenciales);
            await _context.SaveChangesAsync();

            return Ok(credenciales);
        }

        private bool credencialesExists(int id)
        {
            return _context.credenciales.Any(e => e.Id == id);
        }
    }
}