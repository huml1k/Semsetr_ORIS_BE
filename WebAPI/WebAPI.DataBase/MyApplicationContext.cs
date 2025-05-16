using FastkartAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.DataBase
{
    public class MyApplicationContext : DbContext
    {
        public DbSet<ItemStore> itemStores { get; set; }

        public DbSet<UserModel> users { get; set; }

        public MyApplicationContext(DbContextOptions<MyApplicationContext> dbContext) : base(dbContext) { } 

    }
}
