using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MediatR;
using Hangfire;
using FluentValidation;
using AutoGo.Infrastructure.Data.Context;
using AutoGo.Domain.Models;
using AutoGo.Application.Common.Context;

namespace AutoGo.Infrastructure.Extentions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                   .LogTo(Console.WriteLine, LogLevel.Information);
            });
            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());


            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("Redis");
            });
            services.AddHangfire((sp, config) =>
            {
                var connection = sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
                config.UseSqlServerStorage(connection);
            });
            services.AddHangfireServer();




            // services.AddScoped<IUsersServices<UsersModel>, UsersServices<UsersModel>();

            return services;
        }
    }
}
