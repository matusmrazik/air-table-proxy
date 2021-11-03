using System;

namespace AirTableProxy.WebAPI.Dtos.MessageDtos
{
    public record GetMessagesRequest(int? MaxCount);

    public record MessageRequest(string Title, string Text);

    public record MessageResponse(string Id, string Title, string Text, DateTime? ReceivedAt);
}
