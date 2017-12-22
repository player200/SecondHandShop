namespace SecondHandShop.Service.Admin.Models.Users
{
    using SecondHandShop.Common.Mapping;
    using SecondHandShop.Data.Models;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}