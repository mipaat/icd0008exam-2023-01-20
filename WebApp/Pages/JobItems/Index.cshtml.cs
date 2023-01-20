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
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<JobItem> JobItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.JobItems != null)
            {
                JobItem = await _context.JobItems
                .Include(j => j.Item)
                .Include(j => j.Job).ToListAsync();
            }
        }
    }
}
