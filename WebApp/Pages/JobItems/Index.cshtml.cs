using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.JobItems
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<JobItem> JobItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.JobItems != null)
            {
                JobItem = await _context.JobItems
                .Include(j => j.Item)
                .Include(j => j.Job).ToListAsync();
            }
        }
    }
}
