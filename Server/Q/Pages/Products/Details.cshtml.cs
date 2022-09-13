﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Model;

namespace Q.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public DetailsModel(Q.Data.QContext context)
        {
            _context = context;
        }

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
    }
}
