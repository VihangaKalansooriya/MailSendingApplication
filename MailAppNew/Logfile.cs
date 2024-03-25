using MailSendingApp;
using Serilog;
using System;

public static class Logger
{
    private static readonly ILogger Log = new LoggerConfiguration()
        .WriteTo.File(Globalconfig.logfilepath,
        rollingInterval: RollingInterval.Day
        )
        .CreateLogger();

    public static void LogInformation(string message)
    {
        Log.Information(message);
    }

    public static void LogError(string message, Exception ex)
    {
        Log.Error(ex, message);
    }
}