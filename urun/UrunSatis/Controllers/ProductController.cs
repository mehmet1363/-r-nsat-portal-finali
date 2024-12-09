using AutoMapper;
using urun.Models;
using urun.Repositories;
using urun.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace urun.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _env;

        public ProductController(ProductRepository productRepository, 
            CategoryRepository categoryRepository,
            IMapper mapper, 
            INotyfService notyf,
            IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _notyf = notyf;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetProductsWithCategory();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model);

                if (model.Image != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }

                    product.ImagePath = "/uploads/" + uniqueFileName;
                }

                await _productRepository.AddAsync(product);
                _notyf.Success("Ürün başarıyla eklendi.");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetProductWithCategory(id);
            if (product == null)
                return NotFound();

            var model = _mapper.Map<ProductModel>(product);
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.CategoryId = model.CategoryId;

                if (model.Image != null)
                {
                    // Eski resmi sil
                    if (!string.IsNullOrEmpty(product.ImagePath))
                    {
                        var oldImagePath = Path.Combine(_env.WebRootPath, product.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);
                    }

                    // Yeni resmi kaydet
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }

                    product.ImagePath = "/uploads/" + uniqueFileName;
                }

                await _productRepository.UpdateAsync(product);
                _notyf.Success("Ürün başarıyla güncellendi.");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetProductWithCategory(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    var imagePath = Path.Combine(_env.WebRootPath, product.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                        System.IO.File.Delete(imagePath);
                }

                await _productRepository.DeleteAsync(id);
                _notyf.Success("Ürün başarıyla silindi.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
} 