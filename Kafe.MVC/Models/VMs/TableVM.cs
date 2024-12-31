using System.ComponentModel.DataAnnotations;

namespace Kafe.MVC.Models.VMs
{
    public class TableVM
    {
        [Required(ErrorMessage = "Masa seç")]
        public int TableNumber { get; set; }


    }
}
