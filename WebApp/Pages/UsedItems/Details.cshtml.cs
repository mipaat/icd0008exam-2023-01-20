using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_UsedItems
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
