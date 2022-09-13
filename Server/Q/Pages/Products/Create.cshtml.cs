using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Q.Data;
using Q.Model;

namespace Q.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public CreateModel(Q.Data.QContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TblProducts TblProducts { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TblProducts.Add(TblProducts);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
