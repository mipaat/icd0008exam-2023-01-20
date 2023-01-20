using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.PerformedJobs
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
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

            var performedjob = await _context.PerformedJobs.FirstOrDefaultAsync(m => m.Id == id);

            if (performedjob == null)
            {
                return NotFound();
            }
            else 
            {
                PerformedJob = performedjob;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PerformedJobs == null)
            {
                return NotFound();
            }
            var performedjob = await _context.PerformedJobs.FindAsync(id);

            if (performedjob != null)
            {
                PerformedJob = performedjob;
                _context.PerformedJobs.Remove(PerformedJob);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
