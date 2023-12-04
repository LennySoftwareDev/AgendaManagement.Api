using AgendaManagement.Domain.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaManagement.Domain;

public static class DomainRepositoryConfiguration
{
    public static void ConfigurationDomainRepository(this IServiceCollection services)
    {
        services.AddTransient<AgendaManagementRepository, AgendaManagementRepositoryImpl>();
    }
}
