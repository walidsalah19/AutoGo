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
using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Infrastructure.Services.Auth;
using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Infrastructure.Services.Identity;
using AutoGo.Domain.Interfaces.Repo;
using AutoGo.Infrastructure.Reposatories;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Infrastructure.UnitofWork;

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




            //Authintication
             services.AddScoped<ITokenService,TokenServices>();
             services.AddScoped<IAuthServices, AuthServices>();
             services.AddScoped<IUsersServices, UserServices>();


            //ginaric repo
            services.AddScoped(typeof(IBaseReposatory<>),typeof(BaseReposatory<>));
            //unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //token block services
            services.AddScoped<ITokenBlacklistService, TokenBlacklistService>();


            return services;
        }
    }
}
