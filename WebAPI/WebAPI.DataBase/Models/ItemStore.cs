using FastkartAPI.DataBase.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Models
{
    public class ItemStore
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Picture { get; set; }

        public List<TypeItemEnum> TypeItem { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string SKU { get; set; }

        public DateOnly MFG { get; set; }

        public int Stock { get; set; }
    }
}
