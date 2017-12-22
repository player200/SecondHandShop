namespace SecondHandShop.Service.Moderator.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Service.Moderator.Models.Advertisements;
    using SecondHandShop.Web.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class ModeratorAdvertisementService : IModeratorAdvertisementService
    {
        private readonly SecondHandShopDbContext db;

        public ModeratorAdvertisementService(SecondHandShopDbContext db)
        {
            this.db = db;
        }

        public async Task<ModeratorAdvertisementServiceModel> ById(int id)
        {
            return await this.db
                .Advertisements
                .Where(a => a.Id == id)
                .ProjectTo<ModeratorAdvertisementServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var advertisement=this.db.Advertisements.Find(id);

            this.db.Advertisements.Remove(advertisement);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, int categoryId)
        {
            var advertisement = this.db.Advertisements.Find(id);

            advertisement.CategoryId = categoryId;

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var advertisement = await this.db.Advertisements.FindAsync(id);

            if (advertisement==null)
            {
                return false;
            }

            return true;
        }
    }
}