using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using NLog.Common;
using NLog.Targets;
using SharedLibrary.AppSettings;
using System.Text.Json.Serialization;

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
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, dbTarget);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);

            BuildInternalLogger();

            // Apply config           
            LogManager.Configuration = config;

            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("Logger configured");
        }

        private static void BuildInternalLogger()
        {
            InternalLogger.LogLevel = LogLevel.Trace;

            // enable one of the targets: file, console, logwriter:

            //  enable internal logging to a file (absolute or relative path. Don't use layout renderers)
            InternalLogger.LogFile = "logs/internal/log.txt";

            // enable internal logging to the console
            //InternalLogger.LogToConsole = true;

            InternalLogger.LogWriter = new StreamWriter(File.Open("logs/internal/log.txt", FileMode.Append));

        }

        private static DatabaseTarget BuildDatabaseTarget()
        {
            var target = new DatabaseTarget();
            target.Name = "logDb";

            target.KeepConnection = false;

            var settings = Settings.Load();
            target.DBProvider = "Npgsql.NpgsqlConnection, Npgsql";
            target.ConnectionString = settings.ConnectionString;
            target.CommandText = "insert into logs(created_on,message,level,exception,stack_trace,logger) values (CAST(@datetime AS timestamp),@message,@level,@exception,@trace,@logger)";

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
