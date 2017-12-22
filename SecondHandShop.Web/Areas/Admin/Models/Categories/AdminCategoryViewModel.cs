namespace SecondHandShop.Web.Areas.Admin.Models.Categories
{
    using SecondHandShop.Service.Admin.Models.Categories;
    using System.Collections.Generic;

    public class AdminCategoryViewModel
    {
        public IEnumerable<AdminCategoryServiceModel> Categories { get; set; }
    }
}