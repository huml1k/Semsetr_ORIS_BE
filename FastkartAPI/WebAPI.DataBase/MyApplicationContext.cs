using FastkartAPI.DataBase.Configurations;
using FastkartAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.DataBase
{
    public class MyApplicationContext : DbContext
    {
        public DbSet<ItemStore> ItemsStore { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<CartModel> Cart { get; set; }

        public MyApplicationContext(DbContextOptions<MyApplicationContext> dbContext) : base(dbContext) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemStoreConfiguration());
            modelBuilder.ApplyConfiguration(new UserModelConfiguration());
            modelBuilder.ApplyConfiguration(new CartModelConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
