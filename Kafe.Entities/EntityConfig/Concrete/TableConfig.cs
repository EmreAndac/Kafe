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
    public class TableConfig : BaseConfig<Table>
    {
        public override void Configure(EntityTypeBuilder<Table> builder)
        {
            base.Configure(builder);
            builder
               .Property(p => p.TableNumber)
               .IsRequired()
               .HasMaxLength(5);  // Masa numarası 5 karakter

            builder
                .HasIndex(p => p.TableNumber)
                .IsUnique();  // Masa numarasının benzersiz olması 

            builder.HasData(new Table() { Id = 1, TableNumber = "1" });
            builder.HasData(new Table() { Id = 2, TableNumber = "2" });
            builder.HasData(new Table() { Id = 3, TableNumber = "3" });
        }
    }
}
