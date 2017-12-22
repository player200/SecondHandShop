namespace SecondHandShop.Service
{
    using SecondHandShop.Service.Models.Messages;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task Send(
               string content,
               string senderId,
               string receiverId);

        Task<IEnumerable<MessagesAllListingServiceModel>> AllAsync(string username);

        Task<string> ReceiverUserName(int id);

        Task<MessageIndividualServiceModel> ById(int id);

        Task<bool> Exists(int id);

        Task Delete(int id);
    }
}