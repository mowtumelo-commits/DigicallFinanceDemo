using DigicallDemo.Data;
using DigicallDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigicallDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        public InvoicesController(FinanceDbContext context) => _context = context;

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _context.Invoices.ToListAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _context.Invoices.FindAsync(id));
        [HttpPost] public async Task<IActionResult> Post(Invoice inv) { _context.Invoices.Add(inv); await _context.SaveChangesAsync(); return Ok(inv); }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, Invoice inv) { _context.Invoices.Update(inv); await _context.SaveChangesAsync(); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { var x = await _context.Invoices.FindAsync(id); if (x != null) _context.Invoices.Remove(x); await _context.SaveChangesAsync(); return NoContent(); }
    }
}