using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_UsedItems
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UsedItem UsedItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UsedItems == null)
            {
                return NotFound();
            }

            var useditem =  await _context.UsedItems.FirstOrDefaultAsync(m => m.Id == id);
            if (useditem == null)
            {
                return NotFound();
            }
            UsedItem = useditem;
           ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
           ViewData["PerformedJobId"] = new SelectList(_context.PerformedJobs, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UsedItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsedItemExists(UsedItem.Id))
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

        private bool UsedItemExists(int id)
        {
          return (_context.UsedItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
