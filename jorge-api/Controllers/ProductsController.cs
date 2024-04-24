using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jorge_api.Database;
using jorge_api.Model;

namespace jorge_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Products>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Products> GetProducts(int id)
        {
            if (id > 0)
            {
                return new Products();
            }

            var Products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Products == null)
            {
                return new Products();
            }

            return Products;
        }

        [HttpPost]
        public async Task<Products> Create([FromBody] Products create)
        {
            var Products = _context.Products.Add(create);
            await _context.SaveChangesAsync();
            return Products.Entity;
        }

        [HttpPatch]
        public async Task<Products> Edit([FromQuery] int id, [FromBody] Products edit)
        {
            if (id != edit.Id)
                return new Products();

            var entity = await _context.Products.FindAsync(id);

            if (entity == null)
                return new Products();

            entity.Name = edit.Name;
            entity.Price = edit.Price;

            await _context.SaveChangesAsync();

            return entity;
        }

        [HttpDelete]
        public async Task DeleteConfirmed([FromQuery] int id)
        {
            var Products = await _context.Products.FindAsync(id);
            if (Products != null)
            {
                _context.Products.Remove(Products);
            }

            await _context.SaveChangesAsync();
        }
    }
}
