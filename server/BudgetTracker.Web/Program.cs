using BudgetTracker.Data;
using BudgetTracker.Data.Seeding;
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

namespace BudgetTracker.Web
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
            if ("--seed" in args || "-s" in args)
            {
                using (IServiceScope scope = host.Services.CreateScope())
                {
                    IServiceProvider services = scope.ServiceProvider;
                    BudgetTrackerContext context = services.GetRequiredService<BudgetTrackerContext>();
                    BasicSeed seeder = new BasicSeed(context);
                    seeder.Seed();
                }
            }
        }
    }
}
