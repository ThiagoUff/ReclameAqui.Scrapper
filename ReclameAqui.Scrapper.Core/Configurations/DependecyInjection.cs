using Microsoft.Extensions.DependencyInjection;
using ReclameAqui.Scrapper.Domain.Interfaces.Repository;
using ReclameAqui.Scrapper.Domain.Interfaces.Services;
using ReclameAqui.Scrapper.Infra.Repository;
using ReclameAqui.Scrapper.Service.Services;


namespace ReclameAqui.Scrapper.Core.Configurations
{
    public static class DependecyInjection
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<ILiveTimRepository, LiveTimRepository>();
            services.AddTransient<ITimRepository, TimRepository>();
            services.AddTransient<ITimService, TimService>();
        }
    }
}
