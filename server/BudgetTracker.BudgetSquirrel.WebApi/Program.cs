using BudgetTracker.Data.EntityFramework;
using BudgetTracker.Data.EntityFramework.Seeding;
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BudgetTracker.BudgetSquirrel.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();

            Task seedTask = PerormPreflightOperations(host, args);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static async Task PerormPreflightOperations(IWebHost host, string[] args)
        {
            if (args.Contains("--seed") || args.Contains("-s"))
            {
                using (IServiceScope scope = host.Services.CreateScope())
                {
                    IServiceProvider services = scope.ServiceProvider;
                    BudgetTrackerContext context = services.GetRequiredService<BudgetTrackerContext>();
                    IConfiguration appConfig = services.GetRequiredService<IConfiguration>();
                    BasicSeed seeder = new BasicSeed(context, appConfig);
                    await seeder.Seed();
                }
            }
        }
    }
}
