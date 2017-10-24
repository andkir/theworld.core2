using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using theworldcore.Model;
using theworldcore.Services;

namespace theworldcore
{
    public class Startup
    {
        private readonly IHostingEnvironment env;
        private readonly IConfigurationRoot config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            this.env = env;

            config = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(config);
           // services.AddMvc();
            services.AddIdentity<WorldUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<WorldContext>();


            services.ConfigureApplicationCookie(options => options.LoginPath = "/Auth/LogIn");
            services.AddMvc().AddJsonOptions(options => 
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            if (env.IsDevelopment())
            {
                services.AddTransient<IMessageSender, DebugMessageSender>();
            }

            services.AddDbContext<WorldContext>(
                options => options.UseSqlServer(
                    config.GetConnectionString("WorldConnectionString")));

            services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddTransient<WorldDbSeed>();
            services.AddTransient<GeoCoordsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WorldDbSeed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(config => config.MapRoute("Default", "{controller=App}/{action=Index}/{id?}"));
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            seeder.EnsureSeedData().Wait();
        }
    }
}
