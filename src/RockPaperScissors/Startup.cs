using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using RockPaperScissors.Infrastructure;
using RockPaperScissors.Infrastructure.Repositories;
using RockPaperScissors.Infrastructure.Repositories.Abstract;
using RockPaperScissors.Infrastructure.Services;
using RockPaperScissors.Infrastructure.Services.Abstract;
using System.IO;

namespace RockPaperScissors
{
    public class Startup
    {
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;

        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RockPaperScissorsContext>(options =>
                options.UseSqlServer(Configuration["Data:PhotoGalleryConnection:ConnectionString"]));

            // Repositories
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IRoundRepository, RoundRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ILoggingRepository, LoggingRepository>();

            // Services
            services.AddScoped<IMembershipService, MembershipService>();

            // Add MVC services to the services container.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            // this will serve up wwwroot
            app.UseFileServer();

           // AutoMapperConfiguration.Configure()

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{*path}",
                    defaults: new { controller = "home", action = "index" }
                );
            });

            DbInitializer.Initialize(app.ApplicationServices);
        }
    }
}
