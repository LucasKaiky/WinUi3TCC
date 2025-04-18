using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WinMarket.Core;
using WinMarket.Data.services;
using WinMarket.ViewModel.ViewModels;

namespace WinMarket.App
{
    internal static class AppHost
    {
        public static IHost CreateHost() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddCoreServices()
                        .AddDataServices()
                        .AddViewModelServices();
                })
                .Build();
    }

    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // registre interfaces e modelos puros
            services.AddSingleton<IProductService, ProductService>();
            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            // registre repositórios, EF Core, JSON, etc.
            // ex: .AddDbContext<MeuDbContext>(...)
            return services;
        }

        public static IServiceCollection AddViewModelServices(this IServiceCollection services)
        {
            // registre todos os ViewModels
            services.AddTransient<HomePageViewModel>();
            // .AddTransient<ManageProductsViewModel>()
            // .AddTransient<CartPageViewModel>()
            return services;
        }
    }
}
