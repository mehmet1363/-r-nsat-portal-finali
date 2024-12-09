using AspNetCoreHero.ToastNotification.Abstractions;
using urun.Models;
using urun.Repositories;
using urun.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace urun.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly INotyfService _notyf;

        public CategoryController(CategoryRepository categoryRepository, INotyfService notyf)
        {
            _categoryRepository = categoryRepository;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Json(new { data = categories });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };

                await _categoryRepository.AddAsync(category);
                _notyf.Success("Kategori başarıyla eklendi.");

                // success durumunda bir mesaj ekliyoruz
                return Json(new { success = true, message = "Kategori başarıyla eklendi!" });
            }

            // Hata durumunda bir mesaj döndürülüyor
            return Json(new { success = false, message = "Geçersiz veri!" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            _notyf.Success("Kategori başarıyla silindi.");
            return Json(new { success = true , message ="başarıyla silindi"});
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return Json(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsync(model.Id);
                if (category != null)
                {
                    category.Name = model.Name;
                    category.Description = model.Description;
                    category.IsActive = model.IsActive;
                    category.Updated = DateTime.Now;

                    await _categoryRepository.UpdateAsync(category);
                    _notyf.Success("Kategori başarıyla güncellendi.");
                    return Json(new { success = true, message= "Kategori başarıyla güncellendi." });
                }
            }
            return Json(new { success = false, message = "Geçersiz veri!" });
        }
    }
} 