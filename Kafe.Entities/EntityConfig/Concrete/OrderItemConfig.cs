using Kafe.Entities.Entity.Concrete;
using Kafe.Entities.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe.Entities.EntityConfig.Concrete
{
    public class OrderItemConfig : BaseConfig<OrderItem>
    {


        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder
                .Property(p => p.Quantity)
                .IsRequired();  // Adet zorunlu

            builder
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ürün silindiğinde sipariş öğesi silinmesin

            builder
                .HasOne(p => p.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sipariş silindiğinde, ilgili sipariş öğeleri de silinsin
        }
    }
}
