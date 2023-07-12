using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace UltimateAspNet.Extentions;

public static class ServiceExtentions
{
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSqlServer<RepositoryContext>(configuration.GetConnectionString("sqlConnection"));

    public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder) => 
        builder.AddMvcOptions(opt => opt.OutputFormatters.Add(new CsvOutputFormatter()));

}
