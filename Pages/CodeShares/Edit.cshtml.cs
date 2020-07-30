using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment4.Data;
using Assignment4.Models;

namespace Assignment4.Pages.CodeShares
{
    public class EditModel : PageModel
    {
        private readonly Assignment4.Data.AppDbContext _context;

        public EditModel(Assignment4.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CodeShare CodeShare { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CodeShare = await _context.CodeShare.FirstOrDefaultAsync(m => m.Id == id);

            if (CodeShare == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CodeShare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeShareExists(CodeShare.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CodeShareExists(int id)
        {
            return _context.CodeShare.Any(e => e.Id == id);
        }
    }
}
