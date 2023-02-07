using Microsoft.Extensions.Logging;

namespace wallet.infrastructure.Diagnostics;

public static class LoggerCustom
{
    private static readonly Action<ILogger, string, Exception> warningLogger =
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    new EventId(5000, nameof(LogErrorCustom)), "{message}");
    private static readonly Action<ILogger, string, Exception> informationLogger =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(5001, nameof(LogErrorCustom)), "{message}");

    private static readonly Action<ILogger, string, Exception> errorLogger =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(5002, nameof(LogErrorCustom)), "{message}");

    public static void LogWarningCustom(this ILogger logger, string message)
    {
        warningLogger(logger, message, null);
    }

    public static void LogInformationCustom(this ILogger logger, string message)
    {
        informationLogger(logger, message, null);
    }

    public static void LogErrorCustom(this ILogger logger, string message, Exception exception)
    {
        errorLogger(logger, message, exception);
    }
}