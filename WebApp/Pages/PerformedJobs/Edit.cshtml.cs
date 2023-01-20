using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.PerformedJobs
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PerformedJob PerformedJob { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PerformedJobs == null)
            {
                return NotFound();
            }

            var performedjob =  await _context.PerformedJobs.FirstOrDefaultAsync(m => m.Id == id);
            if (performedjob == null)
            {
                return NotFound();
            }
            PerformedJob = performedjob;
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

            _context.Attach(PerformedJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformedJobExists(PerformedJob.Id))
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

        private bool PerformedJobExists(int id)
        {
          return (_context.PerformedJobs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
