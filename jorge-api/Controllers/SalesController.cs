using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jorge_api.Database;
using jorge_api.Model;

namespace jorge_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Sales>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Sales> GetSales(int id)
        {
            if (id > 0)
            {
                return new Sales();
            }

            var Sales = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Sales == null)
            {
                return new Sales();
            }

            return Sales;
        }

        [HttpPost]
        public async Task<Sales> Create([FromBody] Sales create)
        {
            var Sales = _context.Sales.Add(create);
            await _context.SaveChangesAsync();
            return Sales.Entity;
        }

        [HttpPatch]
        public async Task<Sales> Edit([FromQuery] int id, [FromBody] Sales edit)
        {
            if (id != edit.Id)
                return new Sales();

            if (await _context.Sales.FindAsync(id) != null)
                return new Sales();


            var Sales = _context.Update(edit);
            await _context.SaveChangesAsync();

            return Sales.Entity as Sales;
        }

        [HttpDelete]
        public async Task DeleteConfirmed([FromQuery] int id)
        {
            var Sales = await _context.Sales.FindAsync(id);
            if (Sales != null)
            {
                _context.Sales.Remove(Sales);
            }

            await _context.SaveChangesAsync();
        }
    }
}
