using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Model;
namespace Q.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly QContext _context;

        public GamesController(QContext context)
        {
            _context = context;
        }


        // GET: api/Games/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/id/12
        [Route(("id/{id}"))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames(int id)
        {
            return await _context.Games.Where(g => g.UserId == id).ToListAsync();
        }

        // POST: api/Games
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<Game> PostGames(Game game)
        {
            _context.Games.Add(game);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch { }

            return game;
        }

    }
}
