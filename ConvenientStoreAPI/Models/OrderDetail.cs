using System;
using System.Collections.Generic;

namespace ConvenientStoreAPI.Models
{
    public partial class Orderdetail
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int Id { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
