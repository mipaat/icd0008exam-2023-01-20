using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_UsedItems
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
        ViewData["PerformedJobId"] = new SelectList(_context.PerformedJobs, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public UsedItem UsedItem { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UsedItems == null || UsedItem == null)
            {
                return Page();
            }

            _context.UsedItems.Add(UsedItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
