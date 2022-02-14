using Microsoft.Extensions.DependencyInjection;
using NotAlone.Domain.Repositories;

namespace NotAlone.Domain
{
    public static class RepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IQueueItemRepository, QueueItemRepository>();
        }
    }
}