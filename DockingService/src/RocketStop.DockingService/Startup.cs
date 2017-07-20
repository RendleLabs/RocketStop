using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RocketStop.DockingService.Data;

namespace RocketStop.DockingService
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            _env = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (!_env.EnvironmentName.Equals("ef"))
            {
                services.AddDbContextPool<DockingContext>(options => {
                    options.UseNpgsql(Configuration.GetConnectionString("Docking"));
                });
            }
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (!env.EnvironmentName.Equals("ef"))
            {
                var dbContextOptions = new DbContextOptionsBuilder<DockingContext>();
                dbContextOptions.UseNpgsql(Configuration.GetConnectionString("Docking"));
                var migrationHelper = new MigrationHelper(loggerFactory);
                migrationHelper.TryMigrate(dbContextOptions.Options).GetAwaiter().GetResult();
            }
            app.UseMvc();
        }
    }
}
