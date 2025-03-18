using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.Messages;
using Serilog;
using Serilog.Sinks.MSSqlServer;


namespace Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.Logger;

//https://github.com/serilog-mssql/serilog-sinks-mssqlserver
public class MsSqlLogger : LoggerServiceBase
{
    public MsSqlLogger(MsSqlConfiguration mssqlConfiguration) : base(logger: null!)
    {
        if (mssqlConfiguration == null)
        {
            throw new Exception(SerilogMessages.NullOptionsMessage);
        }

        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = mssqlConfiguration.TableName,
            AutoCreateSqlDatabase = mssqlConfiguration.AutoCreateSqlTable
        };

        ColumnOptions columnOptions = new();

        global::Serilog.Core.Logger seriLogConfig = new LoggerConfiguration().WriteTo
            .MSSqlServer(mssqlConfiguration.ConnectionString, sinkOptions, columnOptions: columnOptions)
            .CreateLogger();

        Logger = seriLogConfig;
    }
}