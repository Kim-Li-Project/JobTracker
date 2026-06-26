using JobTracker.Application.Interfaces;
using JobTracker.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace JobTracker.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJobService, JobService>();
        return services;
    }
}