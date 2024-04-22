using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jorge_api.Database;
using jorge_api.Model;

namespace jorge_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Client>> GetClients()
        {
            return await _context.Client.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Client> GetClients(int id)
        {
            if (id > 0)
            {
                return new Client();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);

            if (client == null)
            {
                return new Client();
            }

            return client;
        }

        [HttpPost]
        public async Task<Client> Create([FromBody] Client create)
        {
            var client = _context.Client.Add(create);
            await _context.SaveChangesAsync();
            return client.Entity;
        }

        [HttpPatch]
        public async Task<Client> Edit([FromQuery] int id, [FromBody] Client edit)
        {
            if (id != edit.Id)
                return new Client();

            if (await _context.Client.FindAsync(id) != null)
                return new Client();


            var client = _context.Update(edit);
            await _context.SaveChangesAsync();

            return client.Entity as Client;
        }

        [HttpDelete]
        public async Task DeleteConfirmed([FromQuery] int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }

            await _context.SaveChangesAsync();
        }
    }
}
