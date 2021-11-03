using AirTableProxy.Dtos.AirTable;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirTableProxy.Controllers
{
    [ApiController]
    [Route("messages")]
    [Produces("application/json")]
    public class MessagesController
    {
        private readonly AirTableClient _client;

        public MessagesController(AirTableClient client)
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
