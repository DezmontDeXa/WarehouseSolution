using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Targets;
using SharedLibrary.AppSettings;

namespace SharedLibrary.Logging
{
    public class LoggingConfigurator
    {
        public static void ConfigureLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new FileTarget("logfile") { FileName = $"logs/{DateTime.Now.ToString("yyyy-MM-dd")}.log" };
            var logconsole = new ColoredConsoleTarget("logconsole");
            logconsole.Layout = "${longdate}|${level:uppercase=true}|${logger}|${message:withexception=false}";

            var dbTarget = BuildDatabaseTarget();

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, dbTarget);

            // Apply config           
            LogManager.Configuration = config;
        }

        private static DatabaseTarget BuildDatabaseTarget()
        {
            var target = new DatabaseTarget();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json")
                .Build();

            // Get values from the config given their key and their target type.
            var settings = config.GetSection("Settings").Get<Settings>();

            target.DBProvider = "System.Data.SqlClient";
            target.ConnectionString = settings.ConnectionString;
            target.CommandText = "INSERT INTO Logs(CreatedOn,Message,Level,Exception,StackTrace,Logger) VALUES (@datetime,@message,@level,@exception,@trace,@logger)";

            var param = new DatabaseParameterInfo();
            param.Name = "@datetime";
            param.Layout = "${date}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@message";
            param.Layout = "${message}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@level";
            param.Layout = "${level}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@exception";
            param.Layout = "${exception:format=type}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@trace";
            param.Layout = "${exception:format=stacktrace}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@logger";
            param.Layout = "${logger}";
            target.Parameters.Add(param);

            return target;
        }
    }
}
