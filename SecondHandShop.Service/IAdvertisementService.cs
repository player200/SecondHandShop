namespace SecondHandShop.Service
{
    using SecondHandShop.Service.Models.Advertisements;
    using System;
    using System.Threading.Tasks;

    public interface IAdvertisementService
    {
        Task Create(
            string name,
            string description,
            decimal price,
            DateTime publishDate,
            int categoryId,
            string userId);

        Task<AdvertisementAllServiceModel> AllAsync();

        Task<AdvertisementAllServiceModel> AllAsync(string username);

        Task<AdvertisementAllServiceModel> AllAsync(int categoryId);

        Task<AdvertisementAllServiceModel> GetSixAsync();

        AdvertisementsDetailsServiceModel GetDetails(int id);

        Task<AdvertisementEditServiceModel> ById(int id);

        Task Edit(
                int id,
                string name,
                string description,
                decimal price,
                int categoryId);

        Task Delete(int id);

        Task<bool> Exists(int id);

        Task<string> CreatorUserName(int id);
    }
}