using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AssetsM.Data;

namespace AssetsM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AssetsMContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AssetsMContext") ?? throw new InvalidOperationException("Connection string 'AssetsMContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.IgnoreObsoleteProperties();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
