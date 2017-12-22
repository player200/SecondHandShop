namespace SecondHandShop.Service.Moderator.Models.Advertisements
{
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data.Models;

    public class ModeratorAdvertisementServiceModel : IMapFrom<Advertisement>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}