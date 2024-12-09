using System.ComponentModel.DataAnnotations;

namespace urun.ViewModel
{
    public class ContactModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı zorunludur")]
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mesaj alanı zorunludur")]
        [Display(Name = "Mesajınız")]
        public string Message { get; set; }

        public bool IsRead { get; set; }
        public DateTime Created { get; set; }
    }
} 