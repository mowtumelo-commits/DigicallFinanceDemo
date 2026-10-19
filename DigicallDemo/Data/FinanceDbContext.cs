using DigicallDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DigicallDemo.Data
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Claim> Claims => Set<Claim>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FIX for your red fail warnings - tell SQL Server decimal size
            modelBuilder.Entity<Invoice>().Property(x => x.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<Claim>().Property(x => x.ClaimAmount).HasPrecision(18, 2);
        }
    }
}