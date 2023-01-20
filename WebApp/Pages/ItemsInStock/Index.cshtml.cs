using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_ItemsInStock
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
