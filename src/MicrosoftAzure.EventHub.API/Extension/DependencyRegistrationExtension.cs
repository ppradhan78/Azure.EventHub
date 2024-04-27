using MicrosoftAzure.EventHub.Data.BusinessObject;
using MicrosoftAzure.EventHub.Data.BusinessObjects;
using MicrosoftAzure.EventHub.Data.Core;

namespace MicrosoftAzure.EventGrid.API.Extension
{
    public static class DependencyRegistrationExtension
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection Services)
        {

            Services.AddSingleton<IConfigurationSettings, ConfigurationSettings>();
            Services.AddTransient<IFeedProcessEventHubCore, FeedProcessEventHubCore>();
            Services.AddTransient<IFeedProcessEventHubBo, FeedProcessEventHubBo>();
            return Services;
        }
        public static IServiceCollection AddApiDependencies(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddControllers();
            Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            return Services;
        }
    }
}
