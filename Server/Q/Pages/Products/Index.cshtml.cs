using System;
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
    public class IndexModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public IndexModel(Q.Data.QContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Name { get; set; }

        public IList<TblProducts> TblProducts { get;set; }

        public IList<MyA> MyA { get; set; }

        public async Task OnGetAsync()
        {
            var x =
                from p in _context.TblProducts
                select new MyA { Name = p.Name.ToUpper(), Price = p.Price, Discount = p.Price - 1 };

            MyA = await x.ToListAsync();
        }

    }
}
