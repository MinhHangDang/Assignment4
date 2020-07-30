using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment4.Data;
using Assignment4.Models;

namespace Assignment4.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeSharesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CodeSharesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CodeShares
        [HttpGet]
        public IEnumerable<CodeShare> GetCodeShare()
        {
            return _context.CodeShare;
        }

        // GET: api/CodeShares/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCodeShare([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var codeShare = await _context.CodeShare.FindAsync(id);

            if (codeShare == null)
            {
                return NotFound();
            }

            return Ok(codeShare);
        }

        // PUT: api/CodeShares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeShare([FromRoute] int id, [FromBody] CodeShare codeShare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != codeShare.Id)
            {
                return BadRequest();
            }

            _context.Entry(codeShare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeShareExists(id))
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

        // POST: api/CodeShares
        [HttpPost]
        public async Task<IActionResult> PostCodeShare([FromBody] CodeShare codeShare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CodeShare.Add(codeShare);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCodeShare", new { id = codeShare.Id }, codeShare);
        }

        // DELETE: api/CodeShares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodeShare([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var codeShare = await _context.CodeShare.FindAsync(id);
            if (codeShare == null)
            {
                return NotFound();
            }

            _context.CodeShare.Remove(codeShare);
            await _context.SaveChangesAsync();

            return Ok(codeShare);
        }

        private bool CodeShareExists(int id)
        {
            return _context.CodeShare.Any(e => e.Id == id);
        }
    }
}