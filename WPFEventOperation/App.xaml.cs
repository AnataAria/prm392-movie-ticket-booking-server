using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Interface;
using Services.Service;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WPFEventOperation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public IConfiguration Configuration { get; }

        public App()
        {
            var serviceCollection = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Prn221projectContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DB")));

            services.AddSingleton<ITicketService, TicketService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton(typeof(GenericRepository<>));
            services.AddSingleton<AccountRepository>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<Window1>();
            services.AddSingleton<Window2>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
