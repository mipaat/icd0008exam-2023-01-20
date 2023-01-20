using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.PerformedJobs
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
        ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public PerformedJob PerformedJob { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.PerformedJobs == null || PerformedJob == null)
            {
                return Page();
            }

            _context.PerformedJobs.Add(PerformedJob);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
