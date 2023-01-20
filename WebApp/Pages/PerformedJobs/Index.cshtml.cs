using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.PerformedJobs
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<PerformedJob> PerformedJob { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PerformedJobs != null)
            {
                PerformedJob = await _context.PerformedJobs
                .Include(p => p.Job).ToListAsync();
            }
        }
    }
}
