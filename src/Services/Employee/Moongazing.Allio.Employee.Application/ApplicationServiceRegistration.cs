using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Validation;
using Moongazing.Kernel.Application.Rules;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.Logger;
using Moongazing.Kernel.Mailing;
using Moongazing.Kernel.Mailing.MailKitImplementations;
using System.Reflection;

namespace Moongazing.Allio.Employee.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                                                            MailSettings mailSettings,
                                                            MsSqlConfiguration loggerConfig)
    {
        services.AddMapster(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<ILogger, LoggerServiceBase>(_ => new MsSqlLogger(loggerConfig));
        services.AddSingleton<IMailService, MailKitMailService>(_ => new MailKitMailService(mailSettings));






        return services;

    }
    public static IServiceCollection AddSubClassesOfType(this IServiceCollection services,
                                                         Assembly assembly,
                                                         Type type,
                                                         Func<IServiceCollection, Type,
                                                         IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes()
                            .Where(t => t.IsSubclassOf(type) && type != t)
                            .ToList();

        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
    public static IServiceCollection AddMapster(this IServiceCollection services, params Assembly[] assemblies)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(assemblies);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}