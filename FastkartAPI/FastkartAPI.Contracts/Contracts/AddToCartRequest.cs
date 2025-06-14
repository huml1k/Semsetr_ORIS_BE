using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.Contracts.Contracts
{
    public class AddToCartRequest
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
