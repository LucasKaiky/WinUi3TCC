using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System.Diagnostics;

namespace WinMarket.App
{
    public partial class App : Application
    {
        public static IHost Host { get; } = AppHost.CreateHost();

        public App()
        {
            this.InitializeComponent();
            Ioc.Default.ConfigureServices(Host.Services);
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window? m_window;
    }
}
