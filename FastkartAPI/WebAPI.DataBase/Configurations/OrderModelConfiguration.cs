using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Configurations
{
    public class OrderModelConfiguration : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            // Устанавливаем первичный ключ
            builder.HasKey(o => o.OrderId);

            // Генерация ID на стороне приложения (не в БД)
            builder.Property(o => o.OrderId)
                .HasDefaultValueSql("uuid_generate_v4()");

            // Связь с пользователем
            builder.HasOne(o => o.User)
                .WithMany() // У UserModel может быть много заказов
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Ограничение при удалении

            // Индекс для быстрого поиска по трек-номеру
            builder.HasIndex(o => o.TrackingId)
                .IsUnique();
        }
    }
}
