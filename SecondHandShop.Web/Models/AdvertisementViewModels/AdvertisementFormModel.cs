namespace SecondHandShop.Web.Models.AdvertisementViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SecondHandShop.Data;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AdvertisementFormModel
    {
        [Required]
        [MinLength(DataConstants.AdvertisementsNameMinLenth)]
        [MaxLength(DataConstants.AdvertisementsNameMaxLenth)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.AdvertisementsDescriptionMinLenth)]
        [MaxLength(DataConstants.AdvertisementsDescriptionMaxLenth)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}