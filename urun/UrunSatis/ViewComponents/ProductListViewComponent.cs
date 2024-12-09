using urun.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace urun.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly ProductRepository _productRepository;

        public ProductListViewComponent(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productRepository.GetProductsWithCategory();
            return View(products);
        }
    }
} 