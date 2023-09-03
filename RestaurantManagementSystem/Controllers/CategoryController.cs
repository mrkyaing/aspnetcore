using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Models.ViewModels;
using RestaurantManagementSystem.Repostories;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.Controllers {
    public class CategoryController : Controller {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryService _categoryService;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _categoryService =new CategoryService(_categoryRepository);
        }

        public IActionResult List() {
           return View(_categoryService.GetAll());
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Entry() => View();
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Entry(CategoryViewModel viewModel) {
            try {
                _categoryService.Create(viewModel);
                TempData["Msg"] = "1 record is created successfully";
            }
            catch (Exception ex) {
                TempData["Msg"] = "Error occur when record is created because of " + ex.Message;
            }
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string Id) {
            try {
                _categoryService.DeleteById(Id);
                TempData["Msg"] = "delete process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is deleted.";
            }
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string Id) {
            return View(_categoryService.GetById(Id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(CategoryViewModel viewModel) {
            try {
                _categoryService.Update(viewModel);
                TempData["Msg"] = "update process is completed successfully.";
            }
            catch (Exception e) {
                TempData["Msg"] = "error occur when record is updated.";
            }
            return RedirectToAction("List");
        }
    }
}
