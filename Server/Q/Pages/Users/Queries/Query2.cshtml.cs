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
    public class Query2Model : PageModel
    {
        private readonly Q.Data.QContext _context;

        public Query2Model(Q.Data.QContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<User> Users { get; set; }
        [BindProperty]
        public IList<Game> Games { get; set; }

        public async Task OnGetAsync()
        {
            var x =
                from u in _context.Users
                select new User { Name = u.Name, Id = u.Id, PhoneNumber = u.PhoneNumber };
                Users = await x.ToListAsync();

                Users = Users.OrderBy(user => user.Name,
                                    StringComparer.Ordinal).ToList();

            List<Game> lastGames = new List<Game>();
            foreach(User u in Users)
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
                IList <Game> games = await y.ToListAsync();
                List<DateTime> dates = new List<DateTime>();
                if (games.Count != 0)
                {
                    foreach (Game gg in games)
                        dates.Add(DateTime.Parse(gg.GameStartTime));

                    dates.Sort((a, b) => b.CompareTo(a));

                    lastGames.Add(new Game { Id = -1, UserId = -1, GameStartTime =dates.ElementAt(0).ToString(), GameDurationTime = "-" });
                }
                else
                    lastGames.Add(new Game { Id = -1, UserId = -1, GameStartTime = "No Games Played Yet", GameDurationTime = "-" });
            }
            Games = lastGames;
        }
    }
}



