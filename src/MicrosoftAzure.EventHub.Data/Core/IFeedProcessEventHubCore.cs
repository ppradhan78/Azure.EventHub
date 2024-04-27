using MicrosoftAzure.EventGrid.API.Data.SimpleModels;
using System.Threading.Tasks;

namespace MicrosoftAzure.EventHub.Data.Core
{
    public interface IFeedProcessEventHubCore
    {
        Task PushEvent(OrderDetailsSampleModel inputMessage);
        Task<string> PopEvent();
    }
}
