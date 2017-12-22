namespace SecondHandShop.Web.Models.PictureViewModels
{
    using SecondHandShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class PicturesFormModel
    {
        [Required]
        [MinLength(DataConstants.PicturesUrlPathMinLenth)]
        [MaxLength(DataConstants.PicturesUrlPathMaxLenth)]
        [Url]
        [Display(Name = "First picure")]
        public string UrlPathFirst { get; set; }

        [Required]
        [MinLength(DataConstants.PicturesUrlPathMinLenth)]
        [MaxLength(DataConstants.PicturesUrlPathMaxLenth)]
        [Url]
        [Display(Name = "Second picure")]
        public string UrlPathSecond { get; set; }

        [Required]
        [MinLength(DataConstants.PicturesUrlPathMinLenth)]
        [MaxLength(DataConstants.PicturesUrlPathMaxLenth)]
        [Url]
        [Display(Name = "Third picure")]
        public string UrlPathThird { get; set; }

        public int AdvertisementId { get; set; }
    }
}