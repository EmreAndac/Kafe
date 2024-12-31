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
    public class OrderConfig : BaseConfig<Order>
    {


        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.UserId).IsRequired();
            // Kullanıcı adı zorunlu

            builder.Property(p => p.TableId).IsRequired();
            // Masa adı zorunlu 
        }
    }
}
