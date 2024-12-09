using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace urun.ViewModel
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }

        [Display(Name = "Ürün Açıklaması")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Resim")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
    }
} 