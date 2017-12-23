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
        [Display(Name = DataConstants.DisplayFirstUrlPicture)]
        public string UrlPathFirst { get; set; }

        [Required]
        [MinLength(DataConstants.PicturesUrlPathMinLenth)]
        [MaxLength(DataConstants.PicturesUrlPathMaxLenth)]
        [Url]
        [Display(Name = DataConstants.DisplaySecondUrlPicture)]
        public string UrlPathSecond { get; set; }

        [Required]
        [MinLength(DataConstants.PicturesUrlPathMinLenth)]
        [MaxLength(DataConstants.PicturesUrlPathMaxLenth)]
        [Url]
        [Display(Name = DataConstants.DisplayThirdUrlPicture)]
        public string UrlPathThird { get; set; }

        public int AdvertisementId { get; set; }
    }
}