using System;
using System.Collections.Generic;

namespace ConvenientStoreAPI.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Contact { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
