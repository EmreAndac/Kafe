using Kafe.Entities.Entity.Concrete;
using Kafe.Entities.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe.Entities.EntityConfig.Concrete
{
    public class CategoryConfig : BaseConfig<Category>
    {


        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.CategoryName).IsRequired();


            builder.HasData(new Category() { Id = 1, CategoryName = "Yiyecek" });
            builder.HasData(new Category() { Id = 2, CategoryName = "İçecek" });



        }
    }
}
