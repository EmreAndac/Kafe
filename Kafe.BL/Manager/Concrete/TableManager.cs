using Kafe.BL.Manager.Abstract;
using Kafe.Entities.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe.BL.Manager.Concrete
{
    public class TableManager(IManager<Order> orderManager, IManager<Table> tableManager)
    {
        public void AddOrderItem(Product product, int quantity, int orderId)
        {
            // Eğer masada henüz sipariş yoksa yeni bir sipariş oluştur
            var currentOrder = orderManager.GetById(orderId);
            if (currentOrder==null)
            {
                //currentOrder = new Order { Table = this };
                orderManager.Add(currentOrder);
            }

            // Siparişe ürün ekle
            var orderItem = currentOrder.OrderItems.FirstOrDefault(oi => oi.Product == product);
            if (orderItem != null)
            {
                orderItem.Quantity += quantity; // Eğer ürün zaten varsa miktarını arttır
            }
            else
            {
                currentOrder.OrderItems.Add(new OrderItem { Product = product, Quantity = quantity });
            }
        }

        //public void RemoveOrderItem(Product product)
        //{
        //    // Geçerli siparişi bul
        //    var currentOrder = Orders.FirstOrDefault(o => o.OrderItems.Any(oi => oi.Product == product));
        //    if (currentOrder != null)
        //    {
        //        var orderItem = currentOrder.OrderItems.FirstOrDefault(oi => oi.Product == product);
        //        if (orderItem != null)
        //        {
        //            currentOrder.OrderItems.Remove(orderItem); // Ürünü siparişten çıkar
        //        }
        //    }
        //}


    }
}
