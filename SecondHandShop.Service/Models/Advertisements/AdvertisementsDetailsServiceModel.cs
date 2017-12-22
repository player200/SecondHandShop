namespace SecondHandShop.Service.Models.Advertisements
{
    using AutoMapper;
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdvertisementsDetailsServiceModel : IMapFrom<Advertisement>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishDate { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public IEnumerable<Picture> Pictures { get; set; }

        public string NameOfUser { get; set; }

        public string UserName { get; set; }

        public string PhoneNumeber { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                  .CreateMap<Advertisement, AdvertisementsDetailsServiceModel>()
                  .ForMember(a => a.Category, cfg => cfg.MapFrom(a => a.Category.Name))
                  .ForMember(a => a.NameOfUser, cfg => cfg.MapFrom(a => a.User.Name))
                  .ForMember(a => a.UserName, cfg => cfg.MapFrom(a => a.User.UserName))
                  .ForMember(a => a.PhoneNumeber, cfg => cfg.MapFrom(a => a.User.PhoneNumber))
                  .ForMember(a => a.Pictures, cfg => cfg.MapFrom(a => a.Pictures
                                                                         .Where(p => p.AdvertisementId == a.Id)
                                                                         .Select(p => new Picture
                                                                         {
                                                                             Id=p.Id,
                                                                             UrlPath=p.UrlPath,
                                                                             IsPrime=p.IsPrime,
                                                                             AdvertisementId=a.Id
                                                                         })
                                                                         .ToList()));
        }
    }
}