using urun.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace urun.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryListViewComponent(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }
    }
} 