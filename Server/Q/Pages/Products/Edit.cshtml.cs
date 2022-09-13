using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Model;

namespace Q.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public EditModel(Q.Data.QContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TblProducts TblProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblProducts = await _context.TblProducts.FirstOrDefaultAsync(m => m.Id == id);

            if (TblProducts == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TblProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblProductsExists(TblProducts.Id))
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

        private bool TblProductsExists(int id)
        {
            return _context.TblProducts.Any(e => e.Id == id);
        }
    }
}
