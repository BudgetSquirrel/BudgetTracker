using BudgetTracker.BudgetSquirrel.Application;
using BudgetTracker.Common;
using BudgetTracker.Business.Auth;
using BudgetTracker.Data.EntityFramework;
using BudgetTracker.Business.Ports.Repositories;
using BudgetTracker.Data.EntityFramework.Models;
using BudgetTracker.Data.EntityFramework.Repositories;
using BudgetTracker.BudgetSquirrel.WebApi.Authorization;
using BudgetTracker.BudgetSquirrel.WebApi.Data;
using BudgetTracker.BudgetSquirrel.WebApi.Models;
using GateKeeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BudgetTracker.BudgetSquirrel.WebApi
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddSingleton<IConfiguration>(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<BudgetTrackerContext, AppDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Default"));
            });

            services.AddScoped<IAuthenticationApi, AuthenticationApi>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGateKeeperUserRepository<User>, UserRepository>();

            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IBudgetApi, BudgetApi>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "basic-authentication-scheme";

                // you can also skip this to make the challenge scheme handle the forbid as well
                options.DefaultForbidScheme = "basic-authentication-scheme";

                // of course you also need to register that scheme, e.g. using
                options.AddScheme<BasicAuthenticationHandler>("basic-authentication-scheme", "scheme display name");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });



        }
    }
}
