using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using MicrosoftAzure.EventGrid.API.Data.SimpleModels;
using MicrosoftAzure.EventHub.Data.BusinessObject;
using System.Text;
using Newtonsoft.Json;

namespace MicrosoftAzure.EventHub.Data.BusinessObjects
{
    public class FeedProcessEventHubBo : IFeedProcessEventHubBo
    {
        private readonly IConfigurationSettings _configuration;
        public FeedProcessEventHubBo(IConfigurationSettings configuration)
        {
            _configuration = configuration;
        }

        public async Task PushEvent(OrderDetailsSampleModel inputMessage)
        {
            var producerClient = CreateEventHubClient();
            try
            {
                var msg = JsonConvert.SerializeObject(inputMessage);
                EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(msg))))
                {
                    throw new Exception($"Event is too large for the batch and cannot be sent.");
                }
                await producerClient.SendAsync(eventBatch);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                await producerClient.DisposeAsync();
            }
        }
        public async Task<string> PopEvent()
        {
            StringBuilder outputMessage = new StringBuilder();

            var processor = CreateEventProcessorClientClient();

            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            // Start the processing
            await processor.StartProcessingAsync();

            // Wait for 30 seconds for the events to be processed
            await Task.Delay(TimeSpan.FromSeconds(30));

            // Stop the processing
            await processor.StopProcessingAsync();

            Task ProcessEventHandler(ProcessEventArgs eventArgs)
            {
                outputMessage.Append(Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
                return Task.CompletedTask;
            }

            Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
            {
                // Write details about the error 
                outputMessage.Append($"\t Partition '{eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
                outputMessage.Append(eventArgs.Exception.Message);
                return Task.CompletedTask;
            }
            return outputMessage.ToString();
        }


        private EventHubProducerClient CreateEventHubClient()
        {
            return new EventHubProducerClient(_configuration.AzureEventHubConnection, _configuration.AzureEventHubName);
        }
        private EventProcessorClient CreateEventProcessorClientClient()
        {
            BlobContainerClient storageClient = new BlobContainerClient(_configuration.AzureStorageConnection, _configuration.AzureStorageblobContainerName);

            return new EventProcessorClient(storageClient, EventHubConsumerClient.DefaultConsumerGroupName,
            _configuration.AzureEventHubConnection, _configuration.AzureEventHubName);
        }
    }
}
