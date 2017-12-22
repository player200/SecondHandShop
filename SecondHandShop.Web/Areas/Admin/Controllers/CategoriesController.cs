namespace SecondHandShop.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SecondHandShop.Service.Admin;
    using SecondHandShop.Service.Admin.Models.Categories;
    using SecondHandShop.Web.Areas.Admin.Models.Categories;
    using SecondHandShop.Web.Infrastructures.Extentions;
    using System.Threading.Tasks;

    public class CategoriesController : BaseAdminController
    {
        private readonly IAdminCategoryService category;

        public CategoriesController(IAdminCategoryService category)
        {
            this.category = category;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await category.Create(model.Name);

            TempData.AddSuccessMessage(string.Format(WebConstants.SuccessMessageCategoryCreatePlaceholder, model.Name));
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var model = new AdminCategoryViewModel
            {
                Categories = await this.category.AllAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var exists = await this.category.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageCategoryDontExist);
                return RedirectToAction(nameof(All));
            }

            var result = await this.category.ById(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryUsebleServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var exists = await this.category.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageCategoryDontExist);
                return View(model);
            }

            await this.category.Edit(id, model.Name);

            TempData.AddSuccessMessage(string.Format(WebConstants.SuccessMessageCategoryEditPlaceholder, model.Name));
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var exists = await this.category.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageCategoryDontExist);
                return RedirectToAction(nameof(All));
            }

            var result = await this.category.ById(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CategoryUsebleServiceModel model)
        {
            var exists = await this.category.Exists(id);

            if (!exists)
            {
                TempData.AddErrorMessage(WebConstants.ErrorMessageCategoryDontExist);
                return View(model);
            }

            await this.category.Delete(id);

            TempData.AddSuccessMessage(string.Format(WebConstants.SuccessMessageCatogoryDeletePlaceholder, model.Name));
            return RedirectToAction(nameof(All));
        }
    }
}