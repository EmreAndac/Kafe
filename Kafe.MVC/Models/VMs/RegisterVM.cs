using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Kafe.MVC.Models.VMs
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [MinLength(3, ErrorMessage = "En az 3 karakter olmalıdır")]
        [MaxLength(15, ErrorMessage = "En fazla 15 karakter olmalıdır")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Şifre alanı zorunludur")]
        [MinLength(3, ErrorMessage = "En az  karakter olmalıdır")]
        [MaxLength(16, ErrorMessage = "En fazla 16 karakter olmalıdır")]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
