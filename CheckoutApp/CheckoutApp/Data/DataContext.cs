using CheckoutApp.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CheckoutApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Basket> Baskets { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Article>(){HasNoKey};
        //}
    }
}
