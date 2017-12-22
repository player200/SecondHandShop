namespace SecondHandShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Models.Advertisements;
    using SecondHandShop.Web.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdvertisementService : IAdvertisementService
    {
        private readonly SecondHandShopDbContext db;

        public AdvertisementService(SecondHandShopDbContext db)
        {
            this.db = db;
        }

        public async Task<AdvertisementAllServiceModel> AllAsync()
        {
            var advertisements = await this.db
                .Advertisements
                .OrderByDescending(a => a.Id)
                .ProjectTo<AdvertisementsListingServiceModel>()
                .ToListAsync();

            var pictures = await this.db
                .Pictures
                .Where(p => p.IsPrime == true)
                .OrderByDescending(p => p.AdvertisementId)
                .ProjectTo<AdvertisementsAllListingServiceModel>()
                .ToListAsync();

            var model = new AdvertisementAllServiceModel
            {
                Advertisements = advertisements,
                Pictures = this.GetAllPicuturesIsPrime(advertisements, pictures)
            };

            return model;
        }

        public async Task<AdvertisementAllServiceModel> AllAsync(string username)
        {
            var advertisements = await this.db
                .Advertisements
                .Where(a => a.User.UserName == username)
                .OrderByDescending(a => a.Id)
                .ProjectTo<AdvertisementsListingServiceModel>()
                .ToListAsync();

            var pictures = await this.db
                .Pictures
                .Where(p => p.Advertisement.User.UserName == username)
                .Where(p => p.IsPrime == true)
                .OrderByDescending(p => p.AdvertisementId)
                .ProjectTo<AdvertisementsAllListingServiceModel>()
                .ToListAsync();

            var model = new AdvertisementAllServiceModel
            {
                Advertisements = advertisements,
                Pictures = this.GetAllPicuturesIsPrime(advertisements, pictures)
            };

            return model;
        }

        public async Task<AdvertisementAllServiceModel> AllAsync(int categoryId)
        {
            var advertisements = await this.db
                .Advertisements
                .Where(a => a.CategoryId == categoryId)
                .OrderByDescending(a => a.Id)
                .ProjectTo<AdvertisementsListingServiceModel>()
                .ToListAsync();

            var pictures = await this.db
                .Pictures
                .Where(p => p.Advertisement.CategoryId == categoryId)
                .Where(p => p.IsPrime == true)
                .OrderByDescending(p => p.AdvertisementId)
                .ProjectTo<AdvertisementsAllListingServiceModel>()
                .ToListAsync();

            var model = new AdvertisementAllServiceModel
            {
                Advertisements = advertisements,
                Pictures = this.GetAllPicuturesIsPrime(advertisements, pictures)
            };

            return model;
        }

        public async Task<AdvertisementAllServiceModel> GetSixAsync()
        {
            var advertisements = await this.db
                .Advertisements
                .OrderByDescending(a => a.Id)
                .Take(6)
                .ProjectTo<AdvertisementsListingServiceModel>()
                .ToListAsync();

            var pictures = await this.db
                .Pictures
                .Where(p => p.IsPrime == true)
                .OrderByDescending(p => p.AdvertisementId)
                .Take(6)
                .ProjectTo<AdvertisementsAllListingServiceModel>()
                .ToListAsync();

            var model = new AdvertisementAllServiceModel
            {
                Advertisements = advertisements,
                Pictures = this.GetAllPicuturesIsPrime(advertisements, pictures)
            };

            return model;
        }

        public async Task<AdvertisementEditServiceModel> ById(int id)
        {
            return await this.db
                .Advertisements
                .Where(a => a.Id == id)
                .ProjectTo<AdvertisementEditServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task Create(
            string name,
            string description,
            decimal price,
            DateTime publishDate,
            int categoryId,
            string userId)
        {
            var advertisemenrt = new Advertisement
            {
                Name = name,
                Description = description,
                Price = price,
                PublishDate = publishDate,
                CategoryId = categoryId,
                UserId = userId
            };

            this.db.Advertisements.Add(advertisemenrt);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var advertisement = this.db.Advertisements.Find(id);

            if (advertisement == null)
            {
                return;
            }

            this.db.Advertisements.Remove(advertisement);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(
            int id,
            string name,
            string description,
            decimal price,
            int categoryId)
        {
            var advertisement = this.db.Advertisements.Find(id);

            if (advertisement == null)
            {
                return;
            }

            advertisement.Name = name;
            advertisement.Description = description;
            advertisement.Price = price;
            advertisement.CategoryId = categoryId;

            await this.db.SaveChangesAsync();
        }

        public AdvertisementsDetailsServiceModel GetDetails(int id)
        {
            return this.db
                .Advertisements
                .Where(a => a.Id == id)
                .ProjectTo<AdvertisementsDetailsServiceModel>()
                .FirstOrDefault();
        }

        public async Task<bool> Exists(int id)
        {
            var advertisement = await this.db.Advertisements.FindAsync(id);

            if (advertisement == null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> CreatorUserName(int id)
        {
            return await this.db
                .Advertisements
                .Where(a => a.Id == id)
                .Select(a => a.User.UserName)
                .FirstOrDefaultAsync();
        }

        private List<string> GetAllPicuturesIsPrime(List<AdvertisementsListingServiceModel> advertisements,
                                                    List<AdvertisementsAllListingServiceModel> pictures)
        {
            List<string> urls = new List<string>();

            foreach (var advertisement in advertisements)
            {
                if (pictures.Any(p => p.AdvertisementId == advertisement.Id))
                {
                    var url = pictures
                    .Where(p => p.AdvertisementId == advertisement.Id)
                    .Select(p => p.UrlPath)
                    .FirstOrDefault();

                    urls.Add(url);
                }
                else
                {
                    urls.Add("");
                }
            }

            return urls;
        }
    }
}