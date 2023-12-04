using AgendaManagement.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Core;

public static class AplicationServiceConfiguration
{
    public static void ConfigurationAplicationService(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.ConfigurationDomainRepository();
    }
}