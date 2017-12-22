namespace SecondHandShop.Web.Areas.Moderator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SecondHandShop.Service.Admin;
    using SecondHandShop.Service.Moderator;
    using SecondHandShop.Service.Moderator.Models.Advertisements;
    using SecondHandShop.Web.Areas.Moderator.Models.Advertisements;
    using SecondHandShop.Web.Controllers;
    using SecondHandShop.Web.Infrastructures.Extentions;
    using System.Linq;
    using System.Threading.Tasks;

    [Area(WebConstants.ModeratorArea)]
    [Authorize(Roles = WebConstants.ModeratorRole)]
    public class ModAdvertisementsController : Controller
    {
        private readonly IModeratorAdvertisementService advertisement;
        private readonly IAdminCategoryService category;

        public ModAdvertisementsController(
             IModeratorAdvertisementService advertisement,
             IAdminCategoryService category)
        {
            this.advertisement = advertisement;
            this.category = category;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName, new { area = WebConstants.EmptyArea });
            }

            var model = await this.advertisement.ById(id);

            return View(new AdvertisementsEditModeratorViewModel
            {
                Advertisements = model,
                Categories = this.category
                          .All()
                          .Select(c => new SelectListItem
                          {
                              Text = c.Name,
                              Value = c.Id.ToString()
                          })
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdvertisementsEditModeratorViewModel model)
        {
            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName, new { area = WebConstants.EmptyArea });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.advertisement.Edit(
                id,
                model.Advertisements.CategoryId);

            TempData.AddSuccessMessage(WebConstants.SuccessMessageAdvertisementEdit);
            return RedirectToAction(nameof(AdvertisementsController.Details), WebConstants.AdvertisementsControllerName, new { area = WebConstants.EmptyArea, id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName, new { area = WebConstants.EmptyArea });
            }

            var result = await this.advertisement.ById(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ModeratorAdvertisementServiceModel model)
        {
            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName, new { area = WebConstants.EmptyArea });
            }

            await this.advertisement.Delete(id);

            TempData.AddSuccessMessage(WebConstants.SuccessMessageAdvertisementDelete);
            return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName, new { area = WebConstants.EmptyArea });
        }
    }
}