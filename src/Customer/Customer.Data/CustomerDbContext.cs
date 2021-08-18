namespace Sample.App.Customer.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public sealed class CustomerDbContext : DbContext
    {
        public CustomerDbContext() { }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(
                options
            ) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
        )
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(
                    "Data Source=Customers.db;Version=3;Journal Mode=Persist;"
                );
            }

            base.OnConfiguring(
                optionsBuilder
            );
        }
    }
}
