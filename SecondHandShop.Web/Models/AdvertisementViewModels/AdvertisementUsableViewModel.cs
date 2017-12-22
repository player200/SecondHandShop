namespace SecondHandShop.Web.Models.AdvertisementViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SecondHandShop.Service.Models.Advertisements;
    using System.Collections.Generic;

    public class AdvertisementUsableViewModel
    {
        public AdvertisementEditServiceModel Advertisements { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}