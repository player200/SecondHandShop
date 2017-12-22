namespace SecondHandShop.Web.Models.AdvertisementViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class AdvertisementsCategoryFormModel
    {
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}