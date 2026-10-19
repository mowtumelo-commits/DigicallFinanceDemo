using DigicallDemo.Data;
using DigicallDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigicallDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        public ClaimsController(FinanceDbContext context) => _context = context;

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Claims.ToListAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _context.Claims.FindAsync(id));
        [HttpPost] public async Task<IActionResult> Post(Claim c) { _context.Claims.Add(c); await _context.SaveChangesAsync(); return Ok(c); }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, Claim c) { _context.Claims.Update(c); await _context.SaveChangesAsync(); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { var x = await _context.Claims.FindAsync(id); if (x != null) _context.Claims.Remove(x); await _context.SaveChangesAsync(); return NoContent(); }
    }
}