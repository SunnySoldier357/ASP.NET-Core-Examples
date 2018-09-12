using Microsoft.EntityFrameworkCore;

namespace jQueryDatatable.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }
    }
}