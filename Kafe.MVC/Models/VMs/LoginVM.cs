using System.ComponentModel.DataAnnotations;

namespace Kafe.MVC.Models.VMs
{
    public class LoginVM
    {

        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "Name Adresi girilmek zorundadir")]
        //public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre  girilmesi zorunludur")]
        public string Password { get; set; }


    }
}
