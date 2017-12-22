namespace SecondHandShop.Service.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Admin.Models.Categories;
    using SecondHandShop.Web.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminCategoryService : IAdminCategoryService
    {
        private readonly SecondHandShopDbContext db;

        public AdminCategoryService(SecondHandShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminCategoryServiceModel> All()
        {
            return this.db
               .Categories
               .ProjectTo<AdminCategoryServiceModel>()
               .ToList();
        }

        public async Task<IEnumerable<AdminCategoryServiceModel>> AllAsync()
        {
            return await this.db
                .Categories
                .ProjectTo<AdminCategoryServiceModel>()
                .ToListAsync();
        }

        public async Task<CategoryUsebleServiceModel> ById(int id)
        {
            return await this.db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryUsebleServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            db.Categories.Add(category);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await this.db.Categories.FindAsync(id);

            if (category == null)
            {
                return;
            }

            this.db.Categories.Remove(category);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string name)
        {
            var category = await this.db.Categories.FindAsync(id);

            category.Name = name;

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var category = await this.db
                .Categories
                .FindAsync(id);

            if (category == null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> FindName(int id)
        {
            return await this.db
                .Categories
                .Where(c => c.Id == id)
                .Select(c => c.Name)
                .FirstOrDefaultAsync();
        }
    }
}