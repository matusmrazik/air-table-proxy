using AirTableProxy.WebAPI.Business;
using AirTableProxy.WebAPI.Dtos.MessageDtos;
using AirTableProxy.WebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTableProxy.Tests.Helpers
{
    class AirTableClientFake : IAirTableClient
    {
        public static List<MessageResponse> Data { get; }

        static AirTableClientFake()
        {
            Data = new List<MessageResponse> {
                new MessageResponse(Id: "1", Title: "Message 1", Text: "Exception on line 42", ReceivedAt: DateTime.Parse("2021-11-03T23:00:00.000Z")),
                new MessageResponse(Id: "2", Title: "Message 2", Text: "Item not found at index 5", ReceivedAt: DateTime.Parse("2021-11-03T23:01:00.000Z")),
                new MessageResponse(Id: "3", Title: "Message 3", Text: "Object is not initialized", ReceivedAt: DateTime.Parse("2021-11-03T23:02:00.000Z"))
            };
        }

        public Task<MessageResponse> AddMessage(MessageRequest message)
        {
            var response = new MessageResponse(
                Id: UniqueIdGenerator.Generate(),
                Title: message.Title,
                Text: message.Text,
                ReceivedAt: DateTime.Now
            );

            Data.Add(response);
            return Task.FromResult(response);
        }

        public Task<IEnumerable<MessageResponse>> GetMessages(GetMessagesRequest request)
        {
            if (request.MaxCount.HasValue)
            {
                return Task.FromResult<IEnumerable<MessageResponse>>(Data.Take(request.MaxCount.Value).ToList());
            }
            return Task.FromResult<IEnumerable<MessageResponse>>(Data);
        }
    }
}
