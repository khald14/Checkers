using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Model;

namespace Q.Pages.Users.Queries
{
    public class Query5Model : PageModel
    {
        private readonly Q.Data.QContext _context;

        public Query5Model(Q.Data.QContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<User> Users { get; set; }
        [BindProperty]
        public IList<Game> Games { get; set; }
        [BindProperty]
        public IList<int> UserNumberOfGames { get; set; }

        [BindProperty]
        public User user { get; set; }


        public async Task OnGetAsync()
        {

            List<int> gamesCounts = new List<int>();
            var x =
                from u in _context.Users
                select new User { Name = u.Name, Id = u.Id, PhoneNumber = u.PhoneNumber };
            Users = await x.ToListAsync();


            foreach (User u in Users)
            {
                var y =

               from g in _context.Games.Where(p => p.UserId == u.Id)
               select new Game
               {
                   Id = g.Id,
                   UserId = g.UserId,
                   GameStartTime = g.GameStartTime,
                   GameDurationTime = g.GameDurationTime
               };
                Games = await y.ToListAsync();
                if (Games.Count != 0)
                    gamesCounts.Add(Games.Count);
                else
                    gamesCounts.Add(0);

            }
            UserNumberOfGames = gamesCounts;
        }

        public async Task OnPostAsync(string result)
        {
            var y =

              from g in _context.Games.Where(p => p.UserId == user.Id)
              select new Game
              {
                  Id = g.Id,
                  UserId = g.UserId,
                  GameStartTime = g.GameStartTime,
                  GameDurationTime = g.GameDurationTime
              };
            Games = await y.ToListAsync();

            var x =
    from u in _context.Users
    select new User { Name = u.Name, Id = u.Id, PhoneNumber = u.PhoneNumber };
            Users = await x.ToListAsync();
        }
    }
}



