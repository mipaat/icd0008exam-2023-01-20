using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.UsedItems
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<UsedItem> UsedItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UsedItems != null)
            {
                UsedItem = await _context.UsedItems
                .Include(u => u.Item)
                .Include(u => u.PerformedJob).ToListAsync();
            }
        }
    }
}
