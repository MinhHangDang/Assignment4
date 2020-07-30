﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment4.Data;
using Assignment4.Models;

namespace Assignment4.Pages.CodeShares
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment4.Data.AppDbContext _context;

        public DeleteModel(Assignment4.Data.AppDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CodeShare = await _context.CodeShare.FindAsync(id);

            if (CodeShare != null)
            {
                _context.CodeShare.Remove(CodeShare);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
