using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace NotAlone.Services
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVkService, VkService>();
            services.AddScoped<ICreateMessageService, CreateMessageService>();
            services.AddScoped<ILoverHandlerService, LoverHandlerService>();
            services.AddSingleton<IVkApi>(sp => {
                var vkApi = new VkApi();
                vkApi.Authorize(new ApiAuthParams{ AccessToken = configuration["VkApi:AccessToken"] });
                return vkApi;
            });
        }
    }
}