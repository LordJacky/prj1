using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prj1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace prj1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        Prj1DBContext db;

        public GoodsController(Prj1DBContext context)
        {
            db = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Goods>>> Get()
        {
            return await db.Goods.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Goods>> Get(int id)
        {
            Goods product = await db.Goods.FirstOrDefaultAsync(p => p.ProductId == id);
            if(product == null)
                return NotFound();
            return new ObjectResult(product);
        }

        [HttpPost]
        public async Task<ActionResult<Goods>> Post(Goods product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            db.Goods.Add(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]

        public async Task<ActionResult<Goods>> Put(Goods product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (!db.Goods.Any(p => p.ProductId == product.ProductId)) 
            {
                return NotFound();
            }

            db.Update(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Goods>> Delete(int id)
        {
            Goods product = db.Goods.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            db.Goods.Remove(product);
            await db.SaveChangesAsync();
            return Ok(product);

        }
        
    }
}
