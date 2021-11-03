using System;

namespace AirTableProxy.Dtos.AirTable
{
    public record GetMessagesRequest(int? MaxCount);

    public record MessageRequest(string Title, string Text);

    public record MessageResponse(string Id, string Title, string Text, DateTime? ReceivedAt);
}
