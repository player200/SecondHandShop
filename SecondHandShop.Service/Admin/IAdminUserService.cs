namespace SecondHandShop.Service.Admin
{
    using SecondHandShop.Service.Admin.Models.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync(string adminId);
    }
}