using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.JobItems
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

      public JobItem JobItem { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.JobItems == null)
            {
                return NotFound();
            }

            var jobitem = await _context.JobItems.FirstOrDefaultAsync(m => m.Id == id);
            if (jobitem == null)
            {
                return NotFound();
            }
            else 
            {
                JobItem = jobitem;
            }
            return Page();
        }
    }
}
