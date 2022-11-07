using Microsoft.Extensions.DependencyInjection;

namespace Store.Content
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IInfoRepository, InfoRepository>();
            services.AddSingleton<IImageRepository, ImageRepository>();

            return services;
        }
    }
}
