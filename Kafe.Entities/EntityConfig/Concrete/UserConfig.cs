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
    public class UserConfig : BaseConfig<MyUser>
    {


        public override void Configure(EntityTypeBuilder<MyUser> builder)
        {
            base.Configure(builder);
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(10);  // Ad alanı zorunlu ve maksimum 10 karakter

            builder.Property(u => u.Password)
               .IsRequired()
               .HasMaxLength(5);  // Şifre alanı zorunlu ve maksimum 5 karakter

            builder.HasIndex(p => p.Password).IsUnique();

            builder.HasData(new MyUser() { Id = 1, Name = "Esra", Password = "7485" });
        }
    }
}
