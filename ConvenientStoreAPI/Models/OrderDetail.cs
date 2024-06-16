using System;
using System.Collections.Generic;

namespace ConvenientStoreAPI.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public string Quantity { get; set; } = null!;
        public int ProductId { get; set; }
        public int Id { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
