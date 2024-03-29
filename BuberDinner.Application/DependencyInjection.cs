﻿using BuberDinner.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

            return services;
        }
    }
}
