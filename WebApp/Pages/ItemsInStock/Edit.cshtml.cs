using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.ItemsInStock
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
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

            var iteminstock =  await _context.ItemInStocks.FirstOrDefaultAsync(m => m.Id == id);
            if (iteminstock == null)
            {
                return NotFound();
            }
            ItemInStock = iteminstock;
           ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
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

            _context.Attach(ItemInStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemInStockExists(ItemInStock.Id))
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

        private bool ItemInStockExists(int id)
        {
          return (_context.ItemInStocks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
