
using AutoGo.Api.Extentions;
using AutoGo.Api.Middelwares;
using AutoGo.Application.Extentions;
using AutoGo.Infrastructure.Extentions;
using AutoGo.Infrastructure.Seeding;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using AutoGo.Infrastructure.Services.Cloudinary;

namespace AutoGo.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load configuration from appsettings + env vars
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables(); // لازم تبقى موجودة قبل الـ Configure

            // ربط CloudinarySettings
            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("CloudinarySettings"));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddFluentEmail(builder.Configuration);
            builder.Services.AddSwaggerServices();
            builder.Services.AddApiServices();
            builder.Services.AddAuthServices(builder.Configuration);

            builder.Services.AddApplicationServices();
            builder.Host.UseSerilog();

            Console.WriteLine("=== ENVIRONMENT VARIABLES TEST ===");
            Console.WriteLine($"CloudName: {Environment.GetEnvironmentVariable("CLOUDINARY__CloudName")}");
            Console.WriteLine($"ApiKey: {Environment.GetEnvironmentVariable("CLOUDINARY__ApiKey")}");
            Console.WriteLine($"ApiSecret: {Environment.GetEnvironmentVariable("CLOUDINARY__ApiSecret")}");
            Console.WriteLine("=================================");

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

           // app.UseRouting();

            app.ExceptionHandling();

            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<TokenBlacklistMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/admin-jobs");

            app.UseMiniProfiler();

            app.MapControllers();



            app.Run();
        }
    }
}
