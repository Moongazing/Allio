using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.Messages;
using Serilog;

namespace Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.Logger;

public class FileLogger : LoggerServiceBase
{
    public FileLogger(FileLogConfiguration fileLogConfig) : base(logger: null!)
    {
        if (fileLogConfig == null)
        {
            throw new Exception(SerilogMessages.NullOptionsMessage);
        }

        string logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + "." + fileLogConfig.FolderPath, arg1: ".txt");

        Logger = new LoggerConfiguration().WriteTo.File(
            logFilePath, rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: null,
            fileSizeLimitBytes: 5000000,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
            ).CreateLogger();
    }
}
