using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Model;

namespace Q.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblProductsController : ControllerBase
    {
        private readonly QContext _context;

        public TblProductsController(QContext context)
        {
            _context = context;
        }

        // GET: api/TblProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProducts>>> GetTblProducts()
        {
            return await _context.TblProducts.ToListAsync();
        }

        // GET: api/TblProducts/random/number
        [Route("random/{number}")]
        [HttpGet]
        public int Getnumber(int number)
        {
            return number+1000;
        }

        // GET: api/TblProducts/pname/piano
        [Route(("pname/{name}"))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProducts>>> GetTblProducts(string name)
        {
            return await _context.TblProducts.Where(p=>p.Name == name).ToListAsync();
        }

        // GET: api/TblProducts/pname/piano/price
        [Route(("pname/{name}/{price}"))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProducts>>> GetTblProducts(string name, int price)
        {
            return await _context.TblProducts.Where(p => p.Name == name && p.Price > price).OrderByDescending(p=>p.Price).ToListAsync();
        }



        // GET: api/TblProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblProducts>> GetTblProducts(int id)
        {
            var tblProducts = await _context.TblProducts.FindAsync(id);

            if (tblProducts == null)
            {
                return NotFound();
            }

            return tblProducts;
        }

        // PUT: api/TblProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblProducts(int id, TblProducts tblProducts)
        {
            if (id != tblProducts.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TblProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblProducts>> PostTblProducts(TblProducts tblProducts)
        {
            _context.TblProducts.Add(tblProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblProducts", new { id = tblProducts.Id }, tblProducts);
        }

        // DELETE: api/TblProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblProducts>> DeleteTblProducts(int id)
        {
            var tblProducts = await _context.TblProducts.FindAsync(id);
            if (tblProducts == null)
            {
                return NotFound();
            }

            _context.TblProducts.Remove(tblProducts);
            await _context.SaveChangesAsync();

            return tblProducts;
        }

        private bool TblProductsExists(int id)
        {
            return _context.TblProducts.Any(e => e.Id == id);
        }
    }
}
