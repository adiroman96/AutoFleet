using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoFleet.Data;
using AutoFleet.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoFleet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsurancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Insurances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insurance>>> GetInsurances()
        {
            return await _context.Insurances.ToListAsync();
        }

        // GET: api/Insurances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Insurance>> GetInsurance(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);

            if (insurance == null)
            {
                return NotFound();
            }

            return insurance;
        }

        // PUT: api/Insurances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurance(int id, Insurance insurance)
        {
            if (id != insurance.Id)
            {
                return BadRequest();
            }

            _context.Entry(insurance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceExists(id))
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

        // POST: api/Insurances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Insurance>> PostInsurance(Insurance insurance)
        {
            _context.Insurances.Add(insurance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsurance", new { id = insurance.Id }, insurance);
        }

        // DELETE: api/Insurances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Insurance>> DeleteInsurance(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }

            _context.Insurances.Remove(insurance);
            await _context.SaveChangesAsync();

            return insurance;
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurances.Any(e => e.Id == id);
        }
    }
}
