using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jorge_api.Database;
using jorge_api.Model;
using System.Linq;
using System.Reflection;

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

        [HttpPost("create-sale")]
        public async Task CreateSale([FromBody] DTO.CreateSale sale)
        {
            var productId = sale.Products.Select(x => x.ProductId);
            var products = await _context.Products.Where(x => productId.Contains(x.Id)).ToListAsync();

            if (!products.Any())
                return;

            SalesDetails[] sales = [];

            foreach (var product in products)
            {
                var pQty = sale.Products.FirstOrDefault(x => x.ProductId.Equals(product.Id));

                pQty = pQty == null ? new DTO.ProductCount() { Count = 0 } : pQty;

                sales.Append(new SalesDetails()
                {
                    ProductId = product.Id,
                    Quantity = pQty.Count,
                    Subtotal = product.Price * (decimal)pQty.Count,
                });
            }

            var addition = await _context.Sales.AddAsync(new Sales()
            {
                ClientId = sale.ClientId,
                Total = sales.Select(x => x.Subtotal).Sum()
            });

            await _context.SaveChangesAsync();

            foreach (var item in sales)
            {
                item.SaleId = addition.Entity.Id;
            }

            await _context.SalesDetails.AddRangeAsync(sales);

            await _context.SaveChangesAsync();
        }

        [HttpGet("get-report")]
        public async Task<List<Sales>> GetReport()
        {
            var sales = await _context.Sales.Include(x => x.Client).ToListAsync();

            foreach(var sale in sales)
            {
                sale.Details = await _context.SalesDetails.Include(x => x.Product).Where(x => x.SaleId == sale.Id).ToListAsync();
            }
            return sales;
        }
    }
}
