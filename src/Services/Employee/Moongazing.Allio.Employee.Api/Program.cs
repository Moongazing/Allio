using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Moongazing.Allio.Employee.Api.Configurations;
using Moongazing.Allio.Employee.Application;
using Moongazing.Allio.Employee.Persistence;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Extensions;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Moongazing.Kernel.Mailing;
using Moongazing.Kernel.Persistence.MigrationApplier;
using Polly;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


#region Services

builder.Services.AddApplicationServices(builder.Configuration.GetSection("MailSettings").Get<MailSettings>()!,
                                        builder.Configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>()!);


builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);





#endregion
#region Cors
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p =>
    {
        _ = p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    })
);
#endregion
#region Redis

var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
var redisRetryPolicy = Policy
    .Handle<RedisConnectionException>()
    .WaitAndRetry(
    [
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(15)
    ]);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(redisConnectionString!, true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
});
#endregion
#region Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(
        name: "Bearer",
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer YOUR_TOKEN\". \r\n\r\n"
                + "`Enter your token in the text input below.`"
        }
    );
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            Array.Empty<string>()
        }
    });
});
#endregion

var app = builder.Build();
#region Environment

if (app.Environment.IsDevelopment())
{

    app.ConfigureCustomExceptionMiddleware();

    _ = app.UseSwagger();

    _ = app.UseSwaggerUI(opt =>
    {
        opt.DocExpansion(DocExpansion.None);
    });
}
else
{
    app.ConfigureCustomExceptionMiddleware();

    _ = app.UseSwagger();

    _ = app.UseSwaggerUI(opt =>
    {
        opt.DocExpansion(DocExpansion.None);
    });
}
#endregion

#region WebApiConfiguration
const string webApiConfigurationSection = "WebAPIConfiguration";
WebApiConfiguration webApiConfiguration =
    app.Configuration.GetSection(webApiConfigurationSection).Get<WebApiConfiguration>()
    ?? throw new InvalidOperationException($"\"{webApiConfigurationSection}\"{"SectionCannotFoundInConfiguration"}");
#endregion

app.UseCors(opt => opt.WithOrigins(webApiConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod());

app.UseDbMigrationApplier();

app.UseAuthorization();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();


app.Run();