using Kafe.Entities.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe.Entities.Entity.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; } // Yiyecek or İçecek
        public Category Categories { get; set; } // Product ile Category arasında Çoka-Tek ilişkisi
    }
}
