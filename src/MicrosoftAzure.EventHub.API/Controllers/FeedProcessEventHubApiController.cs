using Microsoft.AspNetCore.Mvc;
using MicrosoftAzure.EventGrid.API.Data.SimpleModels;
using MicrosoftAzure.EventHub.Data.BusinessObject;
using MicrosoftAzure.EventHub.Data.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicrosoftAzure.EventHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedProcessEventHubApiController : ControllerBase
    {
        private readonly IConfigurationSettings _configuration;
        private readonly IFeedProcessEventHubCore _feedProcessEventHubCore;
        public FeedProcessEventHubApiController(IConfigurationSettings configuration, IFeedProcessEventHubCore feedProcessEventHubCore)
        {
            _feedProcessEventHubCore = feedProcessEventHubCore;
            _configuration = configuration;
        }
       

        // GET api/<FeedProcessEventHubApiController>/5
        [HttpGet()]
        public async Task<string> Get()
        {
            return await _feedProcessEventHubCore.PopEvent();
        }

        // POST api/<FeedProcessEventHubApiController>
        [HttpPost]
        public void Post([FromBody] OrderDetailsSampleModel value)
        {
            _feedProcessEventHubCore.PushEvent(value);
        }

        //// PUT api/<FeedProcessEventHubApiController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<FeedProcessEventHubApiController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
