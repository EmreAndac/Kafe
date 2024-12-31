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
    public class ProductConfig : BaseConfig<Product>
    {


        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder
                 .Property(p => p.Name).IsRequired().HasMaxLength(100);

            // Ürün adı zorunlu ve maksimum 100 karakter

            builder.Property(p => p.Price).HasColumnType("float");
            // float türü





            builder
                .HasOne(p => p.Categories)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);


            builder.HasData(new Product() { Id = 1, CategoryId = 1, Name = "Çorba", Price = 50 });

            builder.HasData(new Product() { Id = 2, CategoryId = 1, Name = "Makarna", Price = 100 });

            builder.HasData(new Product() { Id = 3, CategoryId = 2, Name = "Kahve", Price = 30 });

            builder.HasData(new Product() { Id = 4, CategoryId = 2, Name = "Çay", Price = 15 });
        }
    }
}
