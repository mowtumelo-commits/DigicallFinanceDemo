using DigicallDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigicallDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly FinanceDbContext _db;
        public ReportsController(FinanceDbContext db) => _db = db;

        [HttpGet("invoices")]
        public async Task<IActionResult> GetInvoicesWithClients()
        {
            var result = await (from inv in _db.Invoices
                                join cli in _db.Clients on inv.ClientId equals cli.Id
                                select new
                                {
                                    inv.Id,
                                    inv.InvoiceNumber,
                                    ClientName = cli.Name,
                                    cli.Company,
                                    inv.Amount,
                                    inv.Status,
                                    inv.DueDate
                                }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetClaimsWithClients()
        {
            var result = await (from claim in _db.Claims
                                join cli in _db.Clients on claim.ClientId equals cli.Id
                                select new
                                {
                                    claim.Id,
                                    ClientName = cli.Name,
                                    cli.Company,
                                    claim.Description,
                                    claim.ClaimAmount,
                                    claim.Status
                                }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetFinanceSummary()
        {
            var summary = await _db.Clients.Select(c => new {
                ClientId = c.Id,
                ClientName = c.Name,
                Company = c.Company,
                Email = c.Email,
                TotalInvoices = _db.Invoices.Where(i => i.ClientId == c.Id).Sum(i => (decimal?)i.Amount) ?? 0,
                TotalClaims = _db.Claims.Where(cl => cl.ClientId == c.Id).Sum(cl => (decimal?)cl.ClaimAmount) ?? 0,
                InvoiceCount = _db.Invoices.Count(i => i.ClientId == c.Id),
                ClaimCount = _db.Claims.Count(cl => cl.ClientId == c.Id),
                Balance = (_db.Invoices.Where(i => i.ClientId == c.Id).Sum(i => (decimal?)i.Amount) ?? 0) - (_db.Claims.Where(cl => cl.ClientId == c.Id).Sum(cl => (decimal?)cl.ClaimAmount) ?? 0)
            }).ToListAsync();
            return Ok(summary);
        }
    }
}