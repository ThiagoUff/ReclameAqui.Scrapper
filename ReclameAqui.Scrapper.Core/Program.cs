using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReclameAqui.Scrapper.Core.Configurations;
using ReclameAqui.Scrapper.Domain.Interfaces.Services;
using ReclameAqui.Scrapper.Service.Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddDependencyInjections();})
    .Build();

//Runner(host.Services);
ITimService service = host.Services.GetRequiredService<ITimService>();
//await service.ExtractInfo();
await service.Scrapper();
//static void Runner(IServiceProvider services)
//{
//    using IServiceScope serviceScope = services.CreateScope();
//    IServiceProvider provider = serviceScope.ServiceProvider;

//    TimService service = provider.GetRequiredService<TimService>();
//    service.Scrapper();
//}