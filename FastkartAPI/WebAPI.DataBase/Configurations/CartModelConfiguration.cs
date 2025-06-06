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
    public class CartModelConfiguration : IEntityTypeConfiguration<CartModel>
    {
        public void Configure(EntityTypeBuilder<CartModel> builder)
        {
            builder.HasKey(c => c.Id);

            // Настройка связи с пользователем
            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка связи с товаром
            builder.HasOne(c => c.Item)
                .WithMany()
                .HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Qty)
                .IsRequired()
                .HasDefaultValue(1);
        }
    }
}
