using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.IoC;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Buffers;
using Infrastructure.CrossCutting.Configuration;

namespace Api
{
  public partial class Startup
  {
      private IConfigurationRoot _configuration { get; }

      public Startup(IHostingEnvironment env)
      {
          var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
          _configuration = builder.Build();
      }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add framework services.

        services.AddMvc(options =>
        {
            options.OutputFormatters.Clear();
            options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings(){
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            }, ArrayPool<char>.Shared));
        });

        Configuration.ConnectionString = _configuration.GetConnectionString("DataAccessMySqlProvider");

        RegisterServices(services);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole(LogLevel.Debug);
        loggerFactory.AddDebug();

        ConfigureAuth(app);

        app.UseStaticFiles();

        app.UseMvc();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        // Adding dependencies from another layers (isolated from Presentation)
        SimpleInjectorBootStrapper.RegisterServices(services);
    }
  }
}