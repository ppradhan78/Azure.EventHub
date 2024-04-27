using MicrosoftAzure.EventGrid.API.Data.SimpleModels;
using MicrosoftAzure.EventHub.Data.BusinessObject;
using MicrosoftAzure.EventHub.Data.BusinessObjects;

namespace MicrosoftAzure.EventHub.Data.Core
{
    public class FeedProcessEventHubCore : IFeedProcessEventHubCore
    {
        private readonly IConfigurationSettings _configuration;
        private readonly IFeedProcessEventHubBo _feedProcessEventHubBo;

        
        public FeedProcessEventHubCore(IConfigurationSettings configuration, IFeedProcessEventHubBo feedProcessEventHubBo)
        {
            _configuration = configuration;
            _feedProcessEventHubBo = feedProcessEventHubBo;
        }

        public Task<string> PopEvent()
        {
          return  _feedProcessEventHubBo.PopEvent();
        }

        public Task PushEvent(OrderDetailsSampleModel inputMessage)
        {
           return _feedProcessEventHubBo.PushEvent(inputMessage);
        }
    }
}
