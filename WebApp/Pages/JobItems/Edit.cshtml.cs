using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.JobItems
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public JobItem JobItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.JobItems == null)
            {
                return NotFound();
            }

            var jobitem =  await _context.JobItems.FirstOrDefaultAsync(m => m.Id == id);
            if (jobitem == null)
            {
                return NotFound();
            }
            JobItem = jobitem;
           ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
           ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Name");
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

            _context.Attach(JobItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobItemExists(JobItem.Id))
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

        private bool JobItemExists(int id)
        {
          return (_context.JobItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
