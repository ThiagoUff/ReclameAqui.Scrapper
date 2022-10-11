using Microsoft.Extensions.DependencyInjection;
using ReclameAqui.Scrapper.Domain.Interfaces.Repository;
using ReclameAqui.Scrapper.Domain.Interfaces.Services;
using ReclameAqui.Scrapper.Infra.Integration;
using ReclameAqui.Scrapper.Infra.Repository;
using ReclameAqui.Scrapper.Service.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReclameAqui.Scrapper.Core.Configurations
{
    public static class DependecyInjection
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<ITimRepository, TimRepository>();
            services.AddTransient<ITimService, TimService>();

            services.AddRefitClient<IReclameAquiIntegration>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://www.globo.com/"));
        }
    }
}
