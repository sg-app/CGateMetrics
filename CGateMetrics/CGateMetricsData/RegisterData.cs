using CGateMetricsData.Interfaces;
using CGateMetricsData.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData
{
    public static class RegisterData
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CGateMetricsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("default"));
            });

            services.AddTransient<IFahrzeugAbfrageService, FahrzeugAbfrageService>();

            return services;
        }


    }
}
