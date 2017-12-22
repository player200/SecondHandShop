namespace SecondHandShop.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service.Admin;
    using SecondHandShop.Web.Areas.Admin.Models.Users;
    using SecondHandShop.Web.Infrastructures.Extentions;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService admin;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(
            IAdminUserService admin,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.admin = admin;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var admin = await this.userManager.GetUserAsync(this.User);
            var adminId = await this.userManager.GetUserIdAsync(admin);

            var users = await this.admin.AllAsync(adminId);
            var roles = this.roleManager
                .Roles
                .Where(r => r.Name != WebConstants.AdministratorRole)
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            var model = new AdminUserListingViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                this.ModelState.AddModelError(string.Empty, WebConstants.ModelStateCustomError);
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var userInRoles = await this.userManager.GetRolesAsync(user);

            if (userInRoles.Any())
            {
                await this.userManager.RemoveFromRolesAsync(user, userInRoles);

                await this.userManager.AddToRoleAsync(user, model.Role);
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage(string.Format(WebConstants.SuccessMessageRoleChangePlaceholder, user.UserName, model.Role));
            return RedirectToAction(nameof(Index));
        }
    }
}