using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.ItemsInStock
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<ItemInStock> ItemInStock { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ItemInStocks != null)
            {
                ItemInStock = await _context.ItemInStocks
                .Include(i => i.Item).ToListAsync();
            }
        }
    }
}
