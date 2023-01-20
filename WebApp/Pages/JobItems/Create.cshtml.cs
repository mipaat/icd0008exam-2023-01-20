using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.JobItems
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
        ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public JobItem JobItem { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.JobItems == null || JobItem == null)
            {
                return Page();
            }

            _context.JobItems.Add(JobItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
