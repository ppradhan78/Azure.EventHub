using MicrosoftAzure.EventGrid.API.Data.SimpleModels;
using System.Threading.Tasks;

namespace MicrosoftAzure.EventHub.Data.BusinessObjects
{
    public interface IFeedProcessEventHubBo
    {
        Task PushEvent(OrderDetailsSampleModel inputMessage);
        Task<string> PopEvent();
    }
}
