namespace SecondHandShop.Service.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using SecondHandShop.Service.Admin.Models.Users;
    using SecondHandShop.Web.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly SecondHandShopDbContext db;

        public AdminUserService(SecondHandShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync(string adminId)
        {
            return await this.db
               .Users
               .Where(u => u.Id != adminId)
               .ProjectTo<AdminUserListingServiceModel>()
               .ToListAsync();
        }
    }
}