using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Abstractions.Jops;
using AutoGo.Application.Abstractions.SendingEmail;
using AutoGo.Application.Common.Context;
using AutoGo.Domain.Interfaces.Repo;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using AutoGo.Infrastructure.Data.Context;
using AutoGo.Infrastructure.Reposatories;
using AutoGo.Infrastructure.Services.Auth;
using AutoGo.Infrastructure.Services.Identity;
using AutoGo.Infrastructure.Services.Jops;
using AutoGo.Infrastructure.Services.SendingEmail;
using AutoGo.Infrastructure.UnitofWork;
using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Abstractions.Cloudinary;
using AutoGo.Infrastructure.Services.Cashing;
using AutoGo.Infrastructure.Services.Cloudinary;

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

            //jops
            services.AddScoped<IBackgroundJops, BackgroundJops>();

            //sending email
            services.AddScoped<ISendingEmailServices, SendingEmailServices>();

            //cashing 
            services.AddScoped<IDealerCashing, DealerCashing>();
            services.AddScoped<IVehicleCacheService, VehicleCacheService>();


            //cloudinary
            services.AddScoped<ICloudinaryServices, CloudinaryServices>();
            return services;
        }
    }
}
