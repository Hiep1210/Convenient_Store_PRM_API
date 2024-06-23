using System;
using System.Collections.Generic;

namespace ConvenientStoreAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            Orderdetails = new HashSet<Orderdetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SupplierId { get; set; }
        public double? Price { get; set; }
        public int CatId { get; set; }
        public int? ImageId { get; set; }

        public virtual Category Cat { get; set; } = null!;
        public virtual Image? Image { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
    }
}
