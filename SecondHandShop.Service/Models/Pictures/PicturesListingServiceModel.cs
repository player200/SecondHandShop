namespace SecondHandShop.Service.Models.Pictures
{
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data;
    using SecondHandShop.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class PicturesListingServiceModel : IMapFrom<Picture>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.PicturesUrlPathMinLenth)]
        [MaxLength(DataConstants.PicturesUrlPathMaxLenth)]
        [Url]
        [Display(Name ="Url Path")]
        public string UrlPath { get; set; }

        public bool IsPrime { get; set; }

        public int AdvertisementId { get; set; }
    }
}