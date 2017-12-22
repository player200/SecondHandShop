namespace SecondHandShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Models.Messages;
    using SecondHandShop.Web.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessageService : IMessageService
    {
        private readonly SecondHandShopDbContext db;

        public MessageService(SecondHandShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<MessagesAllListingServiceModel>> AllAsync(string username)
        {
            return await this.db
                .Messages
                .Where(m => m.Receiver.UserName == username)
                .OrderByDescending(m=>m.MessagedOn)
                .ProjectTo<MessagesAllListingServiceModel>()
                .ToListAsync();
        }

        public async Task<MessageIndividualServiceModel> ById(int id)
        {
            return await this.db
                .Messages
                .Where(m => m.Id == id)
                .ProjectTo<MessageIndividualServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var message = await this.db.Messages.FindAsync(id);

            this.db.Messages.Remove(message);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var message = await this.db.Messages.FindAsync(id);

            if (message==null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> ReceiverUserName(int id)
        {
            return await this.db
                .Messages
                .Where(m => m.Id == id)
                .Select(m => m.Receiver.UserName)
                .FirstOrDefaultAsync();
        }

        public async Task Send(
            string content,
            string senderId,
            string receiverId)
        {
            var message = new Message
            {
                Content = content,
                SenderId = senderId,
                ReceiverId = receiverId,
                MessagedOn = DateTime.UtcNow
            };

            this.db.Messages.Add(message);
            await this.db.SaveChangesAsync();
        }
    }
}