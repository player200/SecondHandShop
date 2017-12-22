namespace SecondHandShop.Service.Admin.Models.Categories
{
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data;
    using SecondHandShop.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class CategoryUsebleServiceModel : IMapFrom<Category>
    {
        [Required]
        [MinLength(DataConstants.CategoriesNameMinLenth)]
        [MaxLength(DataConstants.CategoriesNameMaxLenth)]
        public string Name { get; set; }
    }
}