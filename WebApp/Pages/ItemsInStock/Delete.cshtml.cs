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
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ItemInStock ItemInStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ItemInStocks == null)
            {
                return NotFound();
            }

            var iteminstock = await _context.ItemInStocks.FirstOrDefaultAsync(m => m.Id == id);

            if (iteminstock == null)
            {
                return NotFound();
            }
            else 
            {
                ItemInStock = iteminstock;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ItemInStocks == null)
            {
                return NotFound();
            }
            var iteminstock = await _context.ItemInStocks.FindAsync(id);

            if (iteminstock != null)
            {
                ItemInStock = iteminstock;
                _context.ItemInStocks.Remove(ItemInStock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
