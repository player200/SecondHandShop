namespace SecondHandShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Advertisement
    {
        public int Id { get; set; }

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

        public DateTime PublishDate { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Picture> Pictures { get; set; } = new List<Picture>();

        public string UserId { get; set; }

        public User User { get; set; }
    }
}