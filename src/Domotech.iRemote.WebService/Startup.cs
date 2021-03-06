using Domotech.iRemote.WebService.GraphApi;
using Domotech.iRemote.WebService.Hubs;
using Domotech.iRemote.WebService.Services;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Domotech.iRemote.WebService
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
            services.AddSingleton<IClient, Client>();

            services.Configure<DomotechServiceOptions>(Configuration.GetSection("DomotechServer"));
            services.AddHostedService<DomotechService>();

            services.AddSingleton<IConnectionStateService, ConnectionStateService>();

            services.AddErrorFilter<ErrorFilter>();

            services.AddGraphQL(serviceProvider => SchemaBuilder.New()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddServices(serviceProvider)
                .Create());

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseGraphQL("/api/graph");

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ConnectionStateHub>("/api/hubs/connection");
            });
        }
    }
}
