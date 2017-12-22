namespace SecondHandShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.CategoriesNameMinLenth)]
        [MaxLength(DataConstants.CategoriesNameMaxLenth)]
        public string Name { get; set; }

        public List<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
    }
}