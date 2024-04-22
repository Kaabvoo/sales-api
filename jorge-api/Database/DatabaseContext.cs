using Microsoft.EntityFrameworkCore;

namespace jorge_api.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Model.Client> Client {  get; set; }
        public DbSet<Model.Products> Products { get; set; }
        public DbSet<Model.Sales> Sales { get; set; }
        public DbSet<Model.SalesDetails> SalesDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.Client>().ToTable("Clients");
            modelBuilder.Entity<Model.Products>().ToTable("Products");
            modelBuilder.Entity<Model.Sales>().ToTable("Sales");
            modelBuilder.Entity<Model.SalesDetails>().ToTable("SalesDetails");
        }
    }
}
