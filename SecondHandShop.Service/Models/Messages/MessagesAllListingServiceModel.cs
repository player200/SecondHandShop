namespace SecondHandShop.Service.Models.Messages
{
    using AutoMapper;
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data.Models;
    using System;

    public class MessagesAllListingServiceModel : IMapFrom<Message>, IHaveCustomMapping
    {
        public int Id { get; set; }
        
        public DateTime MessagedOn { get; set; }
        
        public string SenderName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                  .CreateMap<Message, MessagesAllListingServiceModel>()
                  .ForMember(a => a.SenderName, cfg => cfg.MapFrom(a => a.Sender.UserName));
        }
    }
}