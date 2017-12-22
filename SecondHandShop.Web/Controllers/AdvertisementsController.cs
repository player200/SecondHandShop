namespace SecondHandShop.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service;
    using SecondHandShop.Service.Admin;
    using SecondHandShop.Service.Models.Advertisements;
    using SecondHandShop.Web.Infrastructures.Extentions;
    using SecondHandShop.Web.Models.AdvertisementViewModels;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    public class AdvertisementsController : Controller
    {
        private readonly IAdvertisementService advertisement;
        private readonly IAdminCategoryService category;
        private readonly UserManager<User> userManager;

        public AdvertisementsController(
            IAdvertisementService advertisement,
            IAdminCategoryService category,
            UserManager<User> userManager)
        {
            this.advertisement = advertisement;
            this.category = category;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            var model = new AdvertisementFormModel
            {
                Categories = this.category
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Text = c.Name,
                           Value = c.Id.ToString()
                       })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = this.category
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Text = c.Name,
                           Value = c.Id.ToString()
                       });

                return View(model);
            }

            var userId = this.userManager.GetUserId(this.User);

            await advertisement.Create(
                model.Name,
                model.Description,
                model.Price,
                DateTime.UtcNow,
                model.CategoryId,
                userId);

            TempData.AddSuccessMessage(string.Format(WebConstants.SuccessMessageAdvertisementCreate, model.Name));
            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        public IActionResult SelectByCategory()
        {
            var model = new AdvertisementsCategoryFormModel
            {
                Categories = this.category
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Text = c.Name,
                           Value = c.Id.ToString()
                       })
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SelectByCategory(AdvertisementsCategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = this.category
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Text = c.Name,
                           Value = c.Id.ToString()
                       });
                return View(model);
            }

            return RedirectToAction(nameof(AllByCategory), new { categoryId = model.CategoryId });
        }

        public async Task<IActionResult> AllAdvertisements(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageUserDontExist);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
            }

            var model = await this.advertisement.AllAsync(username);

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllByCategory(int categoryId)
        {
            var categoryExists = await this.category.Exists(categoryId);

            if (!categoryExists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageCategoryDontExist);
                return RedirectToAction(nameof(SelectByCategory));
            }

            var model = await this.advertisement.AllAsync(categoryId);
            var categoryName = await this.category.FindName(categoryId);

            return View(new AdvertisementsListingByCategoryServiceModel
            {
                Advertisements = model.Advertisements,
                Pictures = model.Pictures,
                CategoryName = categoryName
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await this.advertisement.AllAsync();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(All));
            }

            var model = this.advertisement.GetDetails(id);

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var creatorUserName = await this.advertisement.CreatorUserName(id);
            var currentUserName = this.User.Identity.Name;

            if (creatorUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(All));
            }

            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(All));
            }

            var model = new AdvertisementUsableViewModel
            {
                Advertisements = await this.advertisement.ById(id),
                Categories = this.category
                          .All()
                          .Select(c => new SelectListItem
                          {
                              Text = c.Name,
                              Value = c.Id.ToString()
                          })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdvertisementUsableViewModel model)
        {
            var creatorUserName = await this.advertisement.CreatorUserName(id);
            var currentUserName = this.User.Identity.Name;

            if (creatorUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                model.Categories = this.category
                          .All()
                          .Select(c => new SelectListItem
                          {
                              Text = c.Name,
                              Value = c.Id.ToString()
                          });
                return View(model);
            }

            await this.advertisement.Edit(
                id,
                model.Advertisements.Name,
                model.Advertisements.Description,
                model.Advertisements.Price,
                model.Advertisements.CategoryId);

            TempData.AddSuccessMessage(string.Format(WebConstants.SuccessMessageAdvertisementEditPlaceholder, model.Advertisements.Name));
            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var creatorUserName = await this.advertisement.CreatorUserName(id);
            var currentUserName = this.User.Identity.Name;

            if (creatorUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(All));
            }

            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(All));
            }

            var result = await this.advertisement.ById(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, AdvertisementEditServiceModel model)
        {
            var creatorUserName = await this.advertisement.CreatorUserName(id);
            var currentUserName = this.User.Identity.Name;

            if (creatorUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(All));
            }

            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(All));
            }

            await this.advertisement.Delete(id);

            TempData.AddSuccessMessage(string.Format(WebConstants.SuccessMessageAdvertisementDeletePlaceholder, model.Name));
            return RedirectToAction(nameof(All));
        }
    }
}