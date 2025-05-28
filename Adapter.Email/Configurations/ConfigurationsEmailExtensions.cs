using Adapter.Email.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Adapter.Email.Configurations
{
    public static class ConfigurationsEmailExtensions
    {
        public static void AddEmail(this IServiceCollection services)
        {
            services.AddScoped<IEmailNotificationAdapter, EmailNotificationAdapter>();
        }
    }
}
