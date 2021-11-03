using AirTableProxy.Tests.Helpers;
using AirTableProxy.WebAPI.Business;
using AirTableProxy.WebAPI.Controllers;
using AirTableProxy.WebAPI.Dtos.MessageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AirTableProxy.Tests.Tests
{
    public class MessagesControllerTest
    {
        private readonly IAirTableClient _client;
        private readonly MessagesController _controller;

        public MessagesControllerTest()
        {
            _client = new AirTableClientFake();
            _controller = new MessagesController(_client);
        }

        [Fact]
        public void TestGetRecords()
        {
            var messages = _controller.GetMessages(new GetMessagesRequest(null)).Result;

            var items = Assert.IsType<List<MessageResponse>>(messages);
            Assert.Equal(AirTableClientFake.Data.Count, items.Count);
        }

        [Fact]
        public void TestGet2Records()
        {
            var messages = _controller.GetMessages(new GetMessagesRequest(2)).Result;

            var items = Assert.IsType<List<MessageResponse>>(messages);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void TestAddRecord()
        {
            var count = AirTableClientFake.Data.Count;
            var newMessage = new MessageRequest(Title: "New message 1", Text: "Test message with very long text");
            var insertedMessage = _controller.AddMessage(newMessage).Result;

            var last = AirTableClientFake.Data.Last();

            Assert.Equal(count + 1, AirTableClientFake.Data.Count);
            Assert.Same(last, insertedMessage);
            Assert.Equal(newMessage.Title, last.Title);
            Assert.Equal(newMessage.Text, last.Text);
        }
    }
}
