using Microsoft.Extensions.Configuration;

namespace MicrosoftAzure.EventHub.Data.BusinessObject
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        #region Global Variable(s)
        private readonly IConfiguration _configuration;
        #endregion

        public ConfigurationSettings(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
        }
        #region Public Prop(s)
        public string AzureEventHubConnection => _configuration["AzureEventHub:EventHubConnection"];
        public string AzureEventHubName => _configuration["AzureEventHub:EventHubName"];
        public string AzureStorageConnection => _configuration["AzureStorage:StorageConnection"];
        public string AzureStorageblobContainerName => _configuration["AzureStorage:blobContainer:ContainerName"];

        #endregion

    }
}
