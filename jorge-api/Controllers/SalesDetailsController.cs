using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jorge_api.Database;
using jorge_api.Model;

namespace jorge_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<SalesDetails>> GetClients()
        {
            return await _context.SalesDetails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<SalesDetails> GetClients(int id)
        {
            if (id > 0)
            {
                return new SalesDetails();
            }

            var SalesDetails = await _context.SalesDetails
                .FirstOrDefaultAsync(m => m.Id == id);

            if (SalesDetails == null)
            {
                return new SalesDetails();
            }

            return SalesDetails;
        }

        [HttpPost]
        public async Task<SalesDetails> Create([FromBody] SalesDetails create)
        {
            var SalesDetails = _context.SalesDetails.Add(create);
            await _context.SaveChangesAsync();
            return SalesDetails.Entity;
        }

        [HttpPatch]
        public async Task<SalesDetails> Edit([FromQuery] int id, [FromBody] SalesDetails edit)
        {
            if (id != edit.Id)
                return new SalesDetails();

            if (await _context.SalesDetails.FindAsync(id) != null)
                return new SalesDetails();


            var SalesDetails = _context.Update(edit);
            await _context.SaveChangesAsync();

            return SalesDetails.Entity as SalesDetails;
        }

        [HttpDelete]
        public async Task DeleteConfirmed([FromQuery] int id)
        {
            var SalesDetails = await _context.SalesDetails.FindAsync(id);
            if (SalesDetails != null)
            {
                _context.SalesDetails.Remove(SalesDetails);
            }

            await _context.SaveChangesAsync();
        }
    }
}
