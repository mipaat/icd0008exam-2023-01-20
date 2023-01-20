using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_PerformedJobs
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<PerformedJob> PerformedJob { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PerformedJobs != null)
            {
                PerformedJob = await _context.PerformedJobs
                .Include(p => p.Job).ToListAsync();
            }
        }
    }
}
