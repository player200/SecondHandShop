namespace SecondHandShop.Service.Models.Messages
{
    using AutoMapper;
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data.Models;
    using System;

    public class MessageIndividualServiceModel : IMapFrom<Message>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime MessagedOn { get; set; }

        public string SenderId { get; set; }

        public string SenderUserName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Message, MessageIndividualServiceModel>()
                .ForMember(a => a.SenderUserName, cfg => cfg.MapFrom(a => a.Sender.UserName));
        }
    }
}