using GSMS.API.PRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSMS.API.PRM
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext
            services.AddDbContext<GsmsContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("GsmsDb")));
            #endregion
            return services;
        }
    }
}
