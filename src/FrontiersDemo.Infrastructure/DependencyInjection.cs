using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Infrastructure.ExternalServices.Frontiers;
using FrontiersDemo.Infrastructure.Persistence;
using FrontiersDemo.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;

namespace FrontiersDemo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("FrontiersDemo"));
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewerInvitationRepository, ReviewerInvitationRepository>();

        services.AddSingleton(TimeProvider.System);

        services.AddOptions<FrontiersApiOptions>()
            .Bind(configuration.GetSection(FrontiersApiOptions.SectionName));

        services.AddHttpClient<IFrontiersOrganizationsClient, FrontiersOrganizationsClient>((_, client) =>
            {
                var opts = configuration.GetSection(FrontiersApiOptions.SectionName).Get<FrontiersApiOptions>()
                           ?? new FrontiersApiOptions();
                client.BaseAddress = new Uri(opts.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(10);
            })
            .AddStandardResilienceHandler();

        return services;
    }
}
