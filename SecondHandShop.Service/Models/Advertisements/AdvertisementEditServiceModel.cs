namespace SecondHandShop.Service.Models.Advertisements
{
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data;
    using SecondHandShop.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class AdvertisementEditServiceModel : IMapFrom<Advertisement>
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
    }
}