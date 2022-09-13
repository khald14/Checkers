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
    public class Query3Model : PageModel
    {
        private readonly Q.Data.QContext _context;

        public Query3Model(Q.Data.QContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Game> Games { get; set; }

        public async Task OnGetAsync()
        {
            var x =
                from g in _context.Games
                select new Game { Id = g.Id, UserId = g.UserId, GameStartTime = g.GameStartTime, GameDurationTime = g.GameDurationTime };
            Games = await x.ToListAsync();
        }
    }
}


