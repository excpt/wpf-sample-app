namespace Sample.App.Order.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class OrderDbContext : DbContext
    {
        public OrderDbContext() { }

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(
                options
            ) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public static OrderDbContext CreateContext(
        ) =>
            new();

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder
        )
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(
                    "Data Source=Orders.db"
                );
            }

            base.OnConfiguring(
                optionsBuilder
            );
        }
    }
}
