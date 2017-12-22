namespace SecondHandShop.Service
{
    using SecondHandShop.Service.Models.Pictures;
    using System.Threading.Tasks;

    public interface IPictureService
    {
        Task Create(
            string urlPathFist,
            bool isPrimeFirst,
            string urlPathSecond,
            bool isPrimeSecond, 
            string urlPathThird,
            bool isPrimeThird,
            int advertisementId);

        Task<int> Count(int advertisementId);

        Task<PicturesListingServiceModel> PicturesIds(int pictureId);

        Task Edit(
            int pictureId,
            string UrlPath);

        Task<bool> Exists(int id);

        Task<string> CreatorUserName(int id);
    }
}