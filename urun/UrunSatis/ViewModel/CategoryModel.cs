using System.ComponentModel.DataAnnotations;

namespace urun.ViewModel
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur")]
        [Display(Name = "Kategori Adı")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        
        [Display(Name = "Aktif Mi?")]
        public bool IsActive { get; set; } = true;
    }
} 