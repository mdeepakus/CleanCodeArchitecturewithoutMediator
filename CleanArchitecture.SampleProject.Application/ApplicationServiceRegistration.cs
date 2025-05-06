using CleanArchitecture.SampleProject.Application.Contracts.Services;
using CleanArchitecture.SampleProject.Application.Services;
using CleanArchitecture.SampleProject.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.SampleProject.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateCategoryService, CreateCategoryService>();
            services.AddScoped<ICreateEventService, CreateEventService>();
            return services;
        }
    }
}
