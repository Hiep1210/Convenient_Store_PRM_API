using System;
using System.Collections.Generic;

namespace ConvenientStoreAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SupplierId { get; set; }
        public string? Image { get; set; }
        public double? Price { get; set; }
        public int CatId { get; set; }

        public virtual Category Cat { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
