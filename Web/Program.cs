using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Common;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppLogger.Init();

            try
            {
                AppLogger.Log("Starting web host");

                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Host terminated unexpectedly {ex.Message}");
            }
            finally
            {
                AppLogger.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseIISIntegration()
                .Build();
    }
}
