using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using WinMarket.Core.Services;
using WinMarket.Data.Services;
using WinMarket.ViewModel.ViewModels;
using WinMarket.ViewModel.ViewModels.auth;

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
            services.AddSingleton<IAuthState, AuthState>();
            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddHttpClient("FakeStore", c =>
            {
                c.BaseAddress = new Uri("https://fakestoreapi.com/");
            });

            services.AddHttpClient<IProductService, ProductService>(c =>
            {
                c.BaseAddress = new Uri("https://fakestoreapi.com/");
            });

            services.AddHttpClient<IAuthService, AuthService>(c =>
            {
                c.BaseAddress = new Uri("https://fakestoreapi.com/");
            });

            services.AddSingleton<ICartService>(sp =>
            {
                var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient("FakeStore");
                var auth = sp.GetRequiredService<IAuthState>();
                var products = sp.GetRequiredService<IProductService>();
                return new CartService(http, auth, products);
            });

            return services;
        }

        public static IServiceCollection AddViewModelServices(this IServiceCollection services)
        {
            services.AddTransient<ManageProductsPageViewModel>();
            services.AddTransient<HomePageViewModel>();
            services.AddTransient<UserProfilePageViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<CartPageViewModel>();
            return services;
        }
    }
}
