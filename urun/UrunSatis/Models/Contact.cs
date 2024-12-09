using System.ComponentModel.DataAnnotations;

namespace urun.Models
{
    public class Contact : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
} 