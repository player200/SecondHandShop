namespace SecondHandShop.Web.Areas.Admin.Models.Categories
{
    using SecondHandShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class CategoryFormModel
    {
        [Required]
        [MinLength(DataConstants.CategoriesNameMinLenth)]
        [MaxLength(DataConstants.CategoriesNameMaxLenth)]
        public string Name { get; set; }
    }
}