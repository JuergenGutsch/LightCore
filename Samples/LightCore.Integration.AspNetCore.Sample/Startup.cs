using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace LightCore.Integration.AspNetCore.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging();
            services.Configure<Person>(o =>
            {
                o.FirstName = "";
                o.LastName = "";
            });
            //services.AddMvc();


        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // {System.Collections.Generic.IEnumerable`1[Microsoft.Extensions.Options.IPostConfigureOptions`1[Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions]] postConfigures}
            builder.RegisterFactory<IEnumerable<IPostConfigureOptions<KestrelServerOptions>>>(c => { return new List<IPostConfigureOptions<KestrelServerOptions>>(); });
            builder.RegisterFactory< IEnumerable<IConfigureOptions<LibuvTransportOptions>>>(x => { return new List<IConfigureOptions<LibuvTransportOptions>>(); });
            builder.RegisterFactory< IEnumerable<IPostConfigureOptions<LibuvTransportOptions>>>(x => { return new List<IPostConfigureOptions<LibuvTransportOptions>>(); });
            builder.RegisterFactory< IEnumerable<IConfigureOptions<ConsoleLoggerOptions>>>(x => { return new List<IConfigureOptions<ConsoleLoggerOptions>>(); });
            builder.RegisterFactory< IEnumerable<IPostConfigureOptions<ConsoleLoggerOptions>>>(x => { return new List<IPostConfigureOptions<ConsoleLoggerOptions>>(); });
            builder.RegisterFactory< IEnumerable<IOptionsChangeTokenSource<ConsoleLoggerOptions>>>(x => { return new List<IOptionsChangeTokenSource<ConsoleLoggerOptions>>(); });

            builder.RegisterFactory<IEnumerable<IConfigureOptions<ApplicationInsightsServiceOptions>>>(x => { return new List<IConfigureOptions<ApplicationInsightsServiceOptions>>(); });
            builder.RegisterFactory<IEnumerable<IPostConfigureOptions<ApplicationInsightsServiceOptions>>>(x => { return new List<IPostConfigureOptions<ApplicationInsightsServiceOptions>>(); });

            // {System.Collections.Generic.IEnumerable`1[Microsoft.ApplicationInsights.AspNetCore.ITelemetryProcessorFactory] telemetryProcessorFactories}
            builder.RegisterFactory<IEnumerable<ITelemetryProcessorFactory>>(x => { return new List<ITelemetryProcessorFactory>(); });

            //{ Void.ctor(
            //    System.Collections.Generic.IEnumerable`1[Microsoft.Extensions.Options.IConfigureOptions`1[Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.LibuvTransportOptions]], 
            //    System.Collections.Generic.IEnumerable`1[Microsoft.Extensions.Options.IPostConfigureOptions`1[Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.LibuvTransportOptions]])}


            //builder.Register<IService, MyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }

    internal class Person
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
    }
}
