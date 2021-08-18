namespace Sample.App.Server.Main
{
    using Customer.Data;
    using Customer.Server;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomerDbContext>(
                options => options.UseSqlite(
                    "Data Source=Customers.db;Version=3;Journal Mode=Persist;"
                )
            );

            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(
                endpoints => { endpoints.MapGrpcService<CustomerService>(); }
            );
        }
    }
}
