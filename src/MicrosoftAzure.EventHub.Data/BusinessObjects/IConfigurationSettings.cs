namespace MicrosoftAzure.EventHub.Data.BusinessObject
{
    public interface IConfigurationSettings
    {
        string AzureEventHubConnection { get; }
        string AzureEventHubName { get; }
        string AzureStorageConnection { get; }
        string AzureStorageblobContainerName { get; }
    }
}
