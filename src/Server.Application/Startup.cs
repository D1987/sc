using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Server.Dal;
using Microsoft.Extensions.Configuration;
using Server.Services.MappingProfiles;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Server.Services.Helpers;
using Server.Services.Interfaces;
using Server.Services.Implementations;

namespace Server.Application
{
    public class Startup
    {
        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ServerContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("SERVER")));

            services.AddAutoMapper(c => c.AddProfile<AppProfile>(), typeof(Startup));
            services.AddAutoMapper(c => c.AddProfile<VMProfile>(), typeof(Startup));

            services
                .AddMvc()
                .AddNewtonsoftJson(options => { 
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ServerContext serverContext)
        {
            if (!env.IsDevelopment())
            {
                // Update database to latest version in for prod mode
                DbInitializer.Initialize(serverContext);
                DbInitializer.Seed(serverContext);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
