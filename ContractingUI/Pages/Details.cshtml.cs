using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContractBC.ContractAggregate;
using Infrastructure.Data;

namespace ContractingUI.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Infrastructure.Data.ContractContext _context;

        public DetailsModel(Infrastructure.Data.ContractContext context)
        {
            _context = context;
        }

      public Contract Contract { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.Include(c=>c.Versions).FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }
            else 
            {
                Contract = contract;
            }
            return Page();
        }
    }
}
