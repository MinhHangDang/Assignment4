using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment4.Data;
using Assignment4.Models;

namespace Assignment4.Pages.CodeShares
{
    public class CreateModel : PageModel
    {
        private readonly Assignment4.Data.AppDbContext _context;

        public CreateModel(Assignment4.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CodeShare CodeShare { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CodeShare.Add(CodeShare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}