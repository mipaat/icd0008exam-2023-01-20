using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.UsedItems
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UsedItem UsedItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UsedItems == null)
            {
                return NotFound();
            }

            var useditem = await _context.UsedItems.FirstOrDefaultAsync(m => m.Id == id);

            if (useditem == null)
            {
                return NotFound();
            }
            else 
            {
                UsedItem = useditem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UsedItems == null)
            {
                return NotFound();
            }
            var useditem = await _context.UsedItems.FindAsync(id);

            if (useditem != null)
            {
                UsedItem = useditem;
                _context.UsedItems.Remove(UsedItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
