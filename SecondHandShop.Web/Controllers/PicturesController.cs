namespace SecondHandShop.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandShop.Data;
    using SecondHandShop.Service;
    using SecondHandShop.Service.Models.Pictures;
    using SecondHandShop.Web.Infrastructures.Extentions;
    using SecondHandShop.Web.Models.PictureViewModels;
    using System.Threading.Tasks;

    [Authorize]
    public class PicturesController : Controller
    {
        private readonly IPictureService picture;
        private readonly IAdvertisementService advertisement;

        public PicturesController(
            IPictureService picture,
            IAdvertisementService advertisement)
        {
            this.picture = picture;
            this.advertisement = advertisement;
        }

        public async Task<IActionResult> Add(int id)
        {
            var creatorUserName = await this.advertisement.CreatorUserName(id);
            var currentUserName = this.User.Identity.Name;

            if (creatorUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName);
            }

            var exists = await this.advertisement.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName);
            }

            var model = new PicturesFormModel
            {
                AdvertisementId = id
            };

            var isValid = await this.picture.Count(id) >= 3;

            if (isValid)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageThreePictureAllowed);
                return RedirectToAction(nameof(AdvertisementsController.Details), WebConstants.AdvertisementsControllerName, new { id = id });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, PicturesFormModel model)
        {
            var creatorUserName = await this.advertisement.CreatorUserName(id);
            var currentUserName = this.User.Identity.Name;

            if (creatorUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName);
            }

            if (!ModelState.IsValid)
            {
                model.AdvertisementId = id;
                return View(model);
            }

            var firstUrlEnding = model.UrlPathFirst.Trim().Substring(model.UrlPathFirst.Length - 4);
            var secondUrlEnding = model.UrlPathSecond.Trim().Substring(model.UrlPathSecond.Length - 4);
            var thirdUrlEnding = model.UrlPathThird.Trim().Substring(model.UrlPathThird.Length - 4);

            var correctPicture = true;
            if (firstUrlEnding != WebConstants.EndsWithJpg
                && firstUrlEnding != WebConstants.EndsWithPng)
            {
                correctPicture = false;
                model.UrlPathFirst = DataConstants.ImgDefoutNotFound;
            }

            if (secondUrlEnding != WebConstants.EndsWithJpg
                 && secondUrlEnding != WebConstants.EndsWithPng)
            {
                correctPicture = false;
                model.UrlPathSecond = DataConstants.ImgDefoutNotFound;
            }

            if (thirdUrlEnding != WebConstants.EndsWithJpg
                 && thirdUrlEnding != WebConstants.EndsWithPng)
            {
                correctPicture = false;
                model.UrlPathThird = DataConstants.ImgDefoutNotFound;
            }

            await picture.Create(
                model.UrlPathFirst,
                true,
                model.UrlPathSecond,
                false,
                model.UrlPathThird,
                false,
                id);

            if (correctPicture)
            {
                TempData.AddSuccessMessage(WebConstants.SuccessMessagePictureAdd);
            }
            else
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessagePicutureWithNoPngOrJpg);
            }
            return RedirectToAction(nameof(AdvertisementsController.Details), WebConstants.AdvertisementsControllerName, new { id = id });
        }

        public async Task<IActionResult> Edit(int pictureId)
        {
            var creatorUserName = await this.picture.CreatorUserName(pictureId);
            var currentUserName = this.User.Identity.Name;

            if (creatorUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName);
            }

            var exists = await this.picture.Exists(pictureId);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessagePictureDontExist);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName);
            }

            var result = await this.picture.PicturesIds(pictureId);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int pictureId, int advertisementId, PicturesListingServiceModel model)
        {
            var creatorUserName = await this.picture.CreatorUserName(pictureId);
            var currentUserName = this.User.Identity.Name;

            if (creatorUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName);
            }

            if (!ModelState.IsValid)
            {
                model.AdvertisementId = advertisementId;
                return View(model);
            }

            var existsPicture = await this.picture.Exists(pictureId);
            var existsAdvertisement = await this.advertisement.Exists(advertisementId);

            if (!existsPicture || !existsAdvertisement)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessagePictureDontExist);
                return RedirectToAction(nameof(AdvertisementsController.All), WebConstants.AdvertisementsControllerName);
            }

            var urlEnding = model.UrlPath.Substring(model.UrlPath.Length - 4);

            var correctPicture = true;
            if (urlEnding != WebConstants.EndsWithJpg
                && urlEnding != WebConstants.EndsWithPng)
            {
                correctPicture = false;
                model.UrlPath = DataConstants.ImgDefoutNotFound;
            }

            await this.picture.Edit(
                    pictureId,
                    model.UrlPath);

            if (correctPicture)
            {
                TempData.AddSuccessMessage(WebConstants.SuccessMessagePicutreEdit);
            }
            else
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessagePicutureWithNoPngOrJpg);
            }
            return RedirectToAction(nameof(AdvertisementsController.Details), WebConstants.AdvertisementsControllerName, new { id = model.AdvertisementId });
        }
    }
}