using AirTableProxy.WebAPI.Business.Dtos.AirTableDtos;
using AirTableProxy.WebAPI.Dtos.MessageDtos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirTableProxy.WebAPI.Business
{
    class AirTableClient : IAirTableClient
    {
        private readonly ServiceConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly JsonNamingPolicy _jsonNamingPolicy;

        public AirTableClient(HttpClient httpClient, IOptions<ServiceConfiguration> options)
        {
            _config = options.Value;
            _httpClient = httpClient;
            _jsonNamingPolicy = new MyJsonNamingPolicy();
            this.ConfigureHttpClient(_httpClient);
        }

        private void ConfigureHttpClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.AirTableKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<MessageResponse>> GetMessages(GetMessagesRequest request)
        {
            var maxCount = request.MaxCount.HasValue ? $"maxRecords={request.MaxCount.Value}&" : "";
            var response = await _httpClient.GetFromJsonAsync<MessagesResponseDto>(
                $"https://api.airtable.com/v0/{_config.AirTableAppId}/Messages?{maxCount}&view=Grid%20view"
            );
            return response.Records.Select(rec => new MessageResponse(
                Id: rec.Fields.Id,
                Title: rec.Fields.Summary,
                Text: rec.Fields.Message,
                ReceivedAt: rec.Fields.ReceivedAt
            ));
        }

        public async Task<MessageResponse> AddMessage(MessageRequest message)
        {
            var msgFields = new MessageInfoDto(
                Id: "1", // TODO generate ID
                Summary: message.Title,
                Message: message.Text,
                ReceivedAt: DateTime.Now
            );

            var request = new MessagesRequestDto(Records: new[] { new MessageRequestDto(Fields: msgFields) });
            var response = await _httpClient.PostAsJsonAsync(
                $"https://api.airtable.com/v0/{_config.AirTableAppId}/Messages",
                request,
                new JsonSerializerOptions() { PropertyNamingPolicy = _jsonNamingPolicy }
            );

            if (response.IsSuccessStatusCode)
            {
                return new MessageResponse(
                    Id: msgFields.Id,
                    Title: message.Title,
                    Text: message.Text,
                    ReceivedAt: msgFields.ReceivedAt
                );
            }
            throw new InvalidOperationException(response.ReasonPhrase);
        }

        private class MyJsonNamingPolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name)
            {
                if (name == "Summary" || name == "Message") return name;
                return JsonNamingPolicy.CamelCase.ConvertName(name);
            }
        }
    }
}
