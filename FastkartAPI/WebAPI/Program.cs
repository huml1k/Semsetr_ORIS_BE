using FastkartAPI.AuthCheck;
using FastkartAPI.Contracts.DTOs;
using FastkartAPI.DataBase.Configurations;
using FastkartAPI.DataBase.Repositories;
using FastkartAPI.DataBase.Repositories.Extensions;
using FastkartAPI.DataBase.Repositories.Interfaces;
using FastkartAPI.Infrastructure.Password;
using FastkartAPI.Middlewares;
using FastkartAPI.Services.Mapping;
using FastkartAPI.Services.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebAPI.DataBase;

namespace FastkartAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(nameof(JwtOption)));

            builder.Services.AddDbContext<MyApplicationContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("MyDbContext")));

            builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

            builder.Services.AddScoped<ProductCardDTO>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<OrderService>();

            builder.Services.AddScoped<JwtOption>();
            builder.Services.AddScoped<JwtProvider>();
            builder.Services.AddScoped<PasswordHasher>();

            builder.Services.AddScoped<MyApplicationContext>();
            builder.Services.AddScoped<ItemStoreConfiguration>();
            builder.Services.AddScoped<UserModelConfiguration>();
            builder.Services.AddScoped<CartModelConfiguration>();
            builder.Services.AddScoped<OrderModelConfiguration>();

            builder.Services.AddScoped<IOrderModelRepository, OrderModelRepository>();
            builder.Services.AddScoped<ICartModelRepository, CartModelRepository>();
            builder.Services.AddScoped<IItemStoreRepository, ItemStoreRepository>();
            builder.Services.AddScoped<IUserModelRepository, UserModelRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapping));
            builder.Services.AddAuthOption(builder.Configuration);

            //builder.Services.AddControllers()
            //        .AddJsonOptions(options =>
            //        {
            //            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //        });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddlware>();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
