using System;
using Serilog;
using Serilog.Events;

namespace Common
{
    public class AppLogger
    {
        public static void Init()
        {
            Serilog.Log.Logger = new Serilog.LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                  .Enrich.FromLogContext()
                  .WriteTo.Console()
                  .WriteTo.RollingFile("logs/log-{Date}.log")
                  .CreateLogger();
        }
        public static void CloseAndFlush()
        {
            Serilog.Log.CloseAndFlush();
        }

        public static void Log(string message)
        {
            Serilog.Log.Information(message);
        }

        public static void Log(string message, params object[] args)
        {
            Serilog.Log.Information(message, args);
        }
    }
}
