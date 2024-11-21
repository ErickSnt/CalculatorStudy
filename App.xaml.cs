using Microsoft.Extensions.DependencyInjection;

namespace CalculatorStudy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Registro dos serviços e ViewModels
            var services = new ServiceCollection();
            services.AddSingleton<MainPageViewModel>();
            services.AddSingleton<MainPage>();

            // Resolve e atribui a MainPage
            var provider = services.BuildServiceProvider();
            MainPage = provider.GetRequiredService<MainPage>();
        }
    }
}