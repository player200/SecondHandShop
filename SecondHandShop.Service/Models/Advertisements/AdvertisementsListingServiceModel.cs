namespace SecondHandShop.Service.Models.Advertisements
{
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data.Models;
    using System;

    public class AdvertisementsListingServiceModel : IMapFrom<Advertisement>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishDate { get; set; }
    }
}