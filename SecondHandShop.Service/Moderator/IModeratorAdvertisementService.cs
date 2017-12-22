namespace SecondHandShop.Service.Moderator
{
    using SecondHandShop.Service.Moderator.Models.Advertisements;
    using System.Threading.Tasks;

    public interface IModeratorAdvertisementService
    {
        Task<ModeratorAdvertisementServiceModel> ById(int id);

        Task Edit(
                int id,
                int categoryId);

        Task Delete(int id);

        Task<bool> Exists(int id);
    }
}