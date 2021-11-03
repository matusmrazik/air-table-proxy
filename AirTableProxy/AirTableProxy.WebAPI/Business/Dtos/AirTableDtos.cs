using System;
using System.Collections.Generic;

namespace AirTableProxy.WebAPI.Business.Dtos.AirTableDtos
{
    public record MessageInfoDto(string Id, string Summary, string Message, DateTime? ReceivedAt);

    public record MessageResponseDto(string Id, MessageInfoDto Fields, DateTime CreatedTime);

    public record MessageRequestDto(MessageInfoDto Fields);

    public record MessagesResponseDto(IEnumerable<MessageResponseDto> Records);

    public record MessagesRequestDto(IEnumerable<MessageRequestDto> Records);
}
