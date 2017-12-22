namespace SecondHandShop.Service.Admin
{
    using SecondHandShop.Service.Admin.Models.Categories;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminCategoryService
    {
        Task Create(string name);

        IEnumerable<AdminCategoryServiceModel> All();

        Task<IEnumerable<AdminCategoryServiceModel>> AllAsync();

        Task<CategoryUsebleServiceModel> ById(int id);

        Task Edit(int id, string name);

        Task Delete(int id);

        Task<bool> Exists(int id);

        Task<string> FindName(int id);
    }
}