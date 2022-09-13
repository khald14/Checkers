using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Model;

namespace Q.Pages.Users.Queries
{
    public class Query1Model : PageModel
    {
        private readonly Q.Data.QContext _context;

        public Query1Model(Q.Data.QContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<User> Users { get; set; }

        public async Task OnGetAsync()
        {
            var x =
                from u in _context.Users
                select new User { Name = u.Name, Id = u.Id, PhoneNumber = u.PhoneNumber };
            Users = await x.ToListAsync();
        }


        public async Task OnPostAsync()
        {
            var x =
            from u in _context.Users
            orderby u.Name
            select new User { Name = u.Name, Id = u.Id, PhoneNumber = u.PhoneNumber };
            Users = await x.ToListAsync();
        }
    }
}


