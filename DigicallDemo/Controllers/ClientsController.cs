using DigicallDemo.Data;
using DigicallDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigicallDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        public ClientsController(FinanceDbContext context) => _context = context;

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Clients.ToListAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _context.Clients.FindAsync(id));
        [HttpPost] public async Task<IActionResult> Post(Client c) { _context.Clients.Add(c); await _context.SaveChangesAsync(); return Ok(c); }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, Client c) { _context.Clients.Update(c); await _context.SaveChangesAsync(); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { var x = await _context.Clients.FindAsync(id); if (x != null) _context.Clients.Remove(x); await _context.SaveChangesAsync(); return NoContent(); }
    }
}