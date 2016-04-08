using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataLayer;

namespace RepositoryLayer
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() :
            base("EshopConnection")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<SqlDbContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderOrderStatus> OrderOrderStatuses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Commit()
        {
            base.SaveChanges();
        }
    }
}