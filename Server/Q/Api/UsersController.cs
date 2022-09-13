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
    public class UsersController : ControllerBase
    {
        private readonly QContext _context;

        public UsersController(QContext context)
        {
            _context = context;
        }

        // GET: api/Users/id/12
        [Route(("id/{id}"))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(int id)
        {
            return await _context.Users.Where(p => p.Id == id).ToListAsync();
        }


        // GET: api/Users/random/number
        [Route("random/{number}")]
        [HttpGet]
        public int GetNumber(int number)
        {
            Random rnd = new Random();

            return rnd.Next(number);
        }

    }

}

