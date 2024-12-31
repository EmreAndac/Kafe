using Kafe.Entities.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe.Entities.Entity.Concrete
{
    public class MyUser : BaseEntity
    {
        public string Name { get; set; }
        public required string Password { get; set; }

    }
}
