namespace SecondHandShop.Test.Services
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Implementations;
    using SecondHandShop.Web.Data;
    using SecondHandShop.Web.Infrastructures.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class MessageServiceTest
    {
        private SecondHandShopDbContext Context
        {
            get
            {
                var dbOptions = new DbContextOptionsBuilder<SecondHandShopDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new SecondHandShopDbContext(dbOptions);
            }
        }

        private IEnumerable<Message> GetTestData()
        {
            return new List<Message>()
            {
                new Message()
                {
                    Id=1,
                    Content="helooooooooooooooooo",
                    ReceiverId="geagedawdwa",
                    Receiver=new User
                    {
                        UserName="pesho"
                    },
                    MessagedOn=DateTime.ParseExact("01/01/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
                    SenderId="wdwdwdwdw",
                    Sender=new User
                    {
                        UserName="tosho"
                    }
                },
                new Message()
                {
                    Id=2,
                    Content="helooooooooooooooooo2",
                    ReceiverId="geagedawdwa2",
                    Receiver=new User
                    {
                        UserName="pesho"
                    },
                    MessagedOn=DateTime.ParseExact("02/01/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
                    SenderId="wdwdwdwdw2",
                    Sender=new User
                    {
                        UserName="tosho2"
                    }
                },
                new Message()
                {
                    Id=3,
                    Content="helooooooooooooooooo3",
                    ReceiverId="geagedawdwa3",
                    Receiver=new User
                    {
                        UserName="pesho3"
                    },
                    MessagedOn=DateTime.ParseExact("01/01/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
                    SenderId="wdwdwdwdw3",
                    Sender=new User
                    {
                        UserName="tosho3"
                    }
                }
            };

        }

        private void PopulateData(SecondHandShopDbContext db)
        {
            db.Messages.AddRange(this.GetTestData());
            db.SaveChanges();
        }

        [Fact]
        public async Task AllAsyncShouldReturnCollectionOfMessagesAndOrderThem()
        {
            //Arrange
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
            var db = this.Context;
            this.PopulateData(db);

            var messagesService = new MessageService(db);
            var reseiverUserName = "pesho";

            // Act
            var result = await messagesService.AllAsync(reseiverUserName);

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 2
                            && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task ReceiverUserNameShouldReturnUserNameOfReceiver()
        {
            //Arrange
            var db = this.Context;
            this.PopulateData(db);

            var messagesService = new MessageService(db);
            var messageId = 1;

            // Act
            var result = await messagesService.ReceiverUserName(messageId);

            // Assert
            result
                .Should()
                .Be("pesho");
        }

        [Fact]
        public async Task ByIdShouldReturnMessageWithThatId()
        {
            //Arrange
            var db = this.Context;
            this.PopulateData(db);

            var messagesService = new MessageService(db);
            var messageId = 1;

            // Act
            var result = await messagesService.ById(messageId);

            // Assert
            result
                .Id
                .Should()
                .Be(messageId);

            result
                .Content
                .Should()
                .Be("helooooooooooooooooo");
        }

        [Fact]
        public async Task ExistsShouldReturnBoolean()
        {
            //Arrange
            var db = this.Context;
            this.PopulateData(db);

            var messagesService = new MessageService(db);
            var messageId = 1;

            // Act
            var result = await messagesService.Exists(messageId);

            // Assert
            result
                .Should()
                .Be(true);
        }

        [Fact]
        public async Task DeleteShouldReturnDbWithourThatItem()
        {
            //Arrange
            var db = this.Context;
            this.PopulateData(db);

            var messagesService = new MessageService(db);
            var messageId = 2;

            // Act
            await messagesService.Delete(messageId);

            // Assert
            db
                .Messages
                .Where(m => m.Id == messageId)
                .FirstOrDefault()
                .Should()
                .Be(null);

            db
                .Messages
                .Count()
                .Should()
                .Be(2);
        }

        [Fact]
        public async Task SendShouldReturnDbWithThatItem()
        {
            //Arrange
            var db = this.Context;

            var messagesService = new MessageService(db);
            var content = "reeeeeeeeeeeeeeeeeeee";
            var senderId = "geageagaegeagaeg";
            var receiverId = "fwfwadfafagaegaeg";

            // Act
            await messagesService.Send(
                content,
                senderId,
                receiverId);

            // Assert
            db
                .Messages
                .Where(m => m.Content == content)
                .FirstOrDefault()
                .Should()
                .NotBe(null);

            db
                .Messages
                .Where(m => m.Content == content)
                .Select(m=>m.SenderId)
                .FirstOrDefault()
                .Should()
                .Be(senderId);

            db
                .Messages
                .Where(m => m.Content == content)
                .Select(m => m.ReceiverId)
                .FirstOrDefault()
                .Should()
                .Be(receiverId);
        }
    }
}