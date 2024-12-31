using System.Collections.Generic;
using Kafe.Entities.Entity.Abstract;

namespace Kafe.Entities.Entity.Concrete
{
    public class Table : BaseEntity
    {
        public string TableNumber { get; set; } // Masa numarası
        public bool IsOccupied { get; set; } = false; // Masa meşgul mü
        public ICollection<Order> Orders { get; set; } = new List<Order>(); // İlgili siparişler

        
    }
}