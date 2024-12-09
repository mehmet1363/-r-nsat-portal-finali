using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace urun.Models
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }

        [Display(Name = "Ürün Açıklaması")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Display(Name = "Fiyat")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Resim")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
} 