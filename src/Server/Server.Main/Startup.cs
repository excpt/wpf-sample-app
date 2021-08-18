namespace Sample.App.Server.Main
{
    using System.IO;

    using Akka.Actor;
    using Akka.Configuration;

    using Customer.Data;
    using Customer.Server;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Order.Data;
    using Order.Server;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var akkaConfig = ConfigurationFactory.ParseString(
                File.ReadAllText(
                    "akka.hcon"
                )
            );

            var actorSystem = ActorSystem.Create(
                "asp-net-system",
                akkaConfig
            );

            var orderService = new AkkaOrderService(
                actorSystem
            );

            services.AddSingleton(
                orderService
            );

            services.AddDbContext<CustomerDbContext>(
                options => options.UseSqlite(
                    "Data Source=Customers.db;"
                )
            );

            services.AddDbContext<OrderDbContext>(
                options => options.UseSqlite(
                    "Data Source=Orders.db;"
                )
            );

            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var customers = serviceScope.ServiceProvider
                    .GetService<CustomerDbContext>();

                customers.Database.EnsureCreated();

                var orders = serviceScope.ServiceProvider
                    .GetService<OrderDbContext>();

                orders.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapGrpcService<CustomerService>();
                    endpoints.MapGrpcService<WebOrderService>();
                }
            );
        }
    }
}
