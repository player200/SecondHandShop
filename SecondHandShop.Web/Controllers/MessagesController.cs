namespace SecondHandShop.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SecondHandShop.Data.Models;
    using SecondHandShop.Service;
    using SecondHandShop.Web.Infrastructures.Extentions;
    using SecondHandShop.Web.Models.MessageViewModels;
    using System.Threading.Tasks;

    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessageService message;
        private readonly UserManager<User> userManager;

        public MessagesController(
            IMessageService message,
            UserManager<User> userManager)
        {
            this.message = message;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Send(string resieverName)
        {
            var senderId = this.userManager.GetUserId(this.User);

            var resiever = await this.userManager.FindByNameAsync(resieverName);
            if (resiever == null)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageUserDontExist);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
            }

            var resieverId = await this.userManager.GetUserIdAsync(resiever);

            if (senderId == resieverId)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageMessageToYourself);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
            }

            return View(new CreateMessageFormViewModel
            {
                ReceiverUserName = resiever.UserName
            });
        }

        [HttpPost]
        public async Task<IActionResult> Send(string resieverName, CreateMessageFormViewModel model)
        {
            var senderId = this.userManager.GetUserId(this.User);

            var resiever = await this.userManager.FindByNameAsync(resieverName);
            if (resiever == null)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageUserDontExist);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
            }

            var resieverId = await this.userManager.GetUserIdAsync(resiever);

            if (senderId == resieverId)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageMessageToYourself);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
            }

            model.ReceiverUserName = resieverName;

            if (!ModelState.IsValid)
            {
                model.ReceiverUserName = resiever.UserName;
                return View(model);
            }

            await this.message.Send(
                model.Content,
                senderId,
                resieverId);

            TempData.AddSuccessMessage(string.Format(WebConstants.SuccessMessageMessageSendPlaceholder, resiever.UserName));
            return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
        }

        public async Task<IActionResult> AllMessages(string username)
        {
            var currentUserName = this.User.Identity.Name;
            if (currentUserName != username)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
            }

            var model = await this.message.AllAsync(username);

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var currentUserName = this.User.Identity.Name;

            var exists = await this.message.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(AllMessages), new { username = currentUserName });
            }

            var receiverUserName = await this.message.ReceiverUserName(id);

            if (receiverUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
            }

            var model = await this.message.ById(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserName = this.User.Identity.Name;

            var exists = await this.message.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageAdvertisementDontExist);
                return RedirectToAction(nameof(AllMessages), new { username = currentUserName });
            }

            var receiverUserName = await this.message.ReceiverUserName(id);

            if (receiverUserName != currentUserName)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageNotAllowedToPage);
                return RedirectToAction(nameof(HomeController.Index), WebConstants.HomeControllerName);
            }

            await this.message.Delete(id);

            TempData.AddSuccessMessage(WebConstants.SuccessMessageMessageDelet);
            return RedirectToAction(nameof(AllMessages),new { username = currentUserName } );
        }
    }
}