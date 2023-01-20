using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_JobItems
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.JobItems == null)
            {
                return NotFound();
            }
            var jobitem = await _context.JobItems.FindAsync(id);

            if (jobitem != null)
            {
                JobItem = jobitem;
                _context.JobItems.Remove(JobItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
