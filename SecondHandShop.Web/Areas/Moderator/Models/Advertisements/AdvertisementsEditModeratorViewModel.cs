namespace SecondHandShop.Web.Areas.Moderator.Models.Advertisements
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SecondHandShop.Service.Moderator.Models.Advertisements;
    using System.Collections.Generic;

    public class AdvertisementsEditModeratorViewModel
    {
        public ModeratorAdvertisementServiceModel Advertisements { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}