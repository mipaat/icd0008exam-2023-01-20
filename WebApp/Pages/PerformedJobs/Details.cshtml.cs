using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.PerformedJobs
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
