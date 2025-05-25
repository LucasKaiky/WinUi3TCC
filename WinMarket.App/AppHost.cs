using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WinMarket.Core.Services;
using WinMarket.Data.Services;
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
            // somente interfaces ou configurações de domínio
            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services
                .AddHttpClient<IProductService, ProductService>(client =>
                {
                    client.BaseAddress = new Uri("https://fakestoreapi.com/");
                });
            return services;
        }

        public static IServiceCollection AddViewModelServices(this IServiceCollection services)
        {
            services.AddTransient<ManageProductsPageViewModel>();
            services.AddTransient<HomePageViewModel>();
            return services;
        }
    }
}
