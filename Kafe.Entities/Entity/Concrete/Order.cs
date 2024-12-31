using Kafe.Entities.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe.Entities.Entity.Concrete
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public MyUser User { get; set; } // Kullanıcıyı ilişkilendiriyoruz
        public int TableId { get; set; }
        public Table Table { get; set; } // Masa bilgisi
        public ICollection<OrderItem> OrderItems { get; set; } // Sipariş edilen ürünler
        public DateTime CreatedDate { get; set; }  // Kayıt oluşturulma tarihi
        public DateTime UpdatedDate { get; set; }  // Son güncelleme tarihi
        public bool IsPayed { get; set; }


    }
}
