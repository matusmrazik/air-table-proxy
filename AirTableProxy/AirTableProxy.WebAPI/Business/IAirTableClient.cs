using AirTableProxy.WebAPI.Dtos.MessageDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirTableProxy.WebAPI.Business
{
    public interface IAirTableClient
    {
        public Task<IEnumerable<MessageResponse>> GetMessages(GetMessagesRequest request);

        public Task<MessageResponse> AddMessage(MessageRequest message);
    }
}
