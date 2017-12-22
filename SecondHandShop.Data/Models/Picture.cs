namespace SecondHandShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        public int Id { get; set; }
        
        [MinLength(DataConstants.PicturesUrlPathMinLenth)]
        [MaxLength(DataConstants.PicturesUrlPathMaxLenth)]
        public string UrlPath { get; set; }

        public bool IsPrime { get; set; }

        public int AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }
    }
}