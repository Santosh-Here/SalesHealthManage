using Microsoft.EntityFrameworkCore;
using SalesHealth.Models;

namespace SalesHealth.DbContexts
{
    public class SalesHealthDbContext:DbContext
    {
        public SalesHealthDbContext(DbContextOptions<SalesHealthDbContext> options) : base(options)
        {
        }
        public DbSet<Sale> Sales { get; set; }
    }
}
