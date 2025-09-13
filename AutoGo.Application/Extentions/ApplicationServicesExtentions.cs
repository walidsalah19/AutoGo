using AutoGo.Application.Behavier;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Extentions
{
    public static class ApplicationServicesExtentions
    {
      
            public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            {
                // التسجيل من خلال Assembly للـ Application
                services.AddMediatR(options =>
                    options.RegisterServicesFromAssembly(typeof(ApplicationServicesExtentions).Assembly));


                services.AddValidatorsFromAssembly(typeof(ApplicationServicesExtentions).Assembly);

                //services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
                // pipeline registration 
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPipline<,>));

                // لو عندك AutoMapper
                services.AddAutoMapper(typeof(ApplicationServicesExtentions).Assembly);

                return services;
            }
    }
}
