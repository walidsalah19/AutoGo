
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
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddSwaggerServices();
            builder.Services.AddApiServices();
            builder.Services.AddAuthServices(builder.Configuration);

            builder.Services.AddApplicationServices();
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

           // app.UseRouting();

            app.ExceptionHandling();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/admin-jobs");

            app.UseMiniProfiler();

            app.MapControllers();



            app.Run();
        }
    }
}
