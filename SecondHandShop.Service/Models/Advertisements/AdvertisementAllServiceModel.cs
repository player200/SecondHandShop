namespace SecondHandShop.Service.Models.Advertisements
{
    using System.Collections.Generic;

    public class AdvertisementAllServiceModel
    {
        public List<AdvertisementsListingServiceModel> Advertisements { get; set; } = new List<AdvertisementsListingServiceModel>();

        public List<string> Pictures { get; set; } = new List<string>();
    }
}