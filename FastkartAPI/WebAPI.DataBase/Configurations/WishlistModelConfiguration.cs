using FastkartAPI.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Configurations
{
    public class WishlistModelConfiguration : IEntityTypeConfiguration<WishListModel>
    {
        public void Configure(EntityTypeBuilder<WishListModel> builder)
        {
            builder.HasKey(w => w.Id);

            // Настройка связи с пользователем
            builder.HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка связи с товаром
            builder.HasOne(w => w.Item)
                .WithMany()
                .HasForeignKey(w => w.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
