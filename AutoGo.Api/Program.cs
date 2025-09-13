
using AutoGo.Api.Extentions;
using AutoGo.Api.Middelwares;
using AutoGo.Infrastructure.Extentions;
using AutoGo.Infrastructure.Seeding;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace AutoGo.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApiServices();
            builder.Services.AddSwaggerServices();
            builder.Services.AddAuthServices(builder.Configuration);
            builder.Host.UseSerilog();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleSeeder.SeedRolesAsync(roleManager);
            }
            app.UseHttpsRedirection();
            app.UseCors("default");


            app.UseMiniProfiler();


            app.UseMiddleware<LoggingMiddleware>();
            app.ExceptionHandling();


            app.UseAuthorization();


            app.UseHangfireDashboard("/admin-jobs");


            app.MapControllers();

            app.Run();
        }
    }
}
