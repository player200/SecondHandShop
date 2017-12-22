namespace SecondHandShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Models.Pictures;
    using SecondHandShop.Web.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class PictureService : IPictureService
    {
        private readonly SecondHandShopDbContext db;

        public PictureService(SecondHandShopDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Count(int advertisementId)
        {
            return await this.db
                .Pictures
                .Where(p => p.AdvertisementId == advertisementId)
                .CountAsync();
        }

        public async Task Create(
            string urlPathFist,
            bool isPrimeFirst,
            string urlPathSecond,
            bool isPrimeSecond,
            string urlPathThird,
            bool isPrimeThird,
            int advertisementId)
        {
            var firstPicture = new Picture
            {
                UrlPath = urlPathFist,
                IsPrime = isPrimeFirst,
                AdvertisementId = advertisementId
            };

            var secondPicture = new Picture
            {
                UrlPath = urlPathSecond,
                IsPrime = isPrimeSecond,
                AdvertisementId = advertisementId
            };

            var thirdPicture = new Picture
            {
                UrlPath = urlPathThird,
                IsPrime = isPrimeThird,
                AdvertisementId = advertisementId
            };

            this.db.Pictures.AddRange(firstPicture, secondPicture, thirdPicture);
            await this.db.SaveChangesAsync();
        }

        public async Task<string> CreatorUserName(int id)
        {
            return await this.db
                .Pictures
                .Where(p => p.Id == id)
                .Select(p=>p.Advertisement.User.UserName)
                .FirstOrDefaultAsync();
        }

        public async Task Edit(
            int pictureId,
            string UrlPath)
        {
            var picture = this.db.Pictures.Find(pictureId);

            picture.UrlPath = UrlPath;

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var picture = await this.db.Pictures.FindAsync(id);

            if (picture==null)
            {
                return false;
            }

            return true;
        }

        public async Task<PicturesListingServiceModel> PicturesIds(int pictureId)
        {
            return await this.db
                .Pictures
                .Where(p => p.Id == pictureId)
                .ProjectTo<PicturesListingServiceModel>()
                .FirstOrDefaultAsync();
        }
    }
}