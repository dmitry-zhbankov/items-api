using System.Net;
using System.Threading.Tasks;
using Items.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Items.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private const string ItemsUiOriginsPolicyName = "ItemsUiOrigins";
        private const string ItemsDbConnectionStringName = "ItemsDb";

        private static readonly string[] AllowedItemsUiOrigins =
        {
            "http://localhost:3000",
            "http://3.143.23.25"
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IItemsRepository>(
                new ItemsRepository(Configuration.GetConnectionString(ItemsDbConnectionStringName)));
            services.AddCors(options =>
            {
                options.AddPolicy(ItemsUiOriginsPolicyName,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            // .WithOrigins(AllowedItemsUiOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseRouting();

            app.UseExceptionHandler(builder => builder.Run(context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Task.CompletedTask;
            }));

            app.UseCors(ItemsUiOriginsPolicyName);

            app.UseHealthChecks("/");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
