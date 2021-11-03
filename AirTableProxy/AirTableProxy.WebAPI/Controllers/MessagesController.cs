using AirTableProxy.WebAPI.Business;
using AirTableProxy.WebAPI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirTableProxy.WebAPI.Controllers
{
    [ApiController]
    [Route("messages")]
    [Produces("application/json")]
    public class MessagesController
    {
        private readonly IAirTableClient _client;

        public MessagesController(IAirTableClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageResponse>> GetMessages([FromQuery] GetMessagesRequest request)
        {
            var messages = await _client.GetMessages(request);
            return messages;
        }

        [HttpPost]
        public async Task<MessageResponse> AddMessage([FromBody] MessageRequest message)
        {
            var inserted = await _client.AddMessage(message);
            return inserted;
        }
    }
}
