namespace SecondHandShop.Service.Models.Advertisements
{
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data.Models;

    public class AdvertisementsAllListingServiceModel : IMapFrom<Picture>
    {
        public string UrlPath { get; set; }

        public int AdvertisementId { get; set; }
    }
}