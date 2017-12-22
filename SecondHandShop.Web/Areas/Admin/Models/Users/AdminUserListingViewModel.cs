namespace SecondHandShop.Web.Areas.Admin.Models.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SecondHandShop.Service.Admin.Models.Users;
    using System.Collections.Generic;

    public class AdminUserListingViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}