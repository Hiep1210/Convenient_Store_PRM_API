using System;
using System.Collections.Generic;

namespace ConvenientStoreAPI.Models
{
    public partial class Image
    {
        public Image()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Url { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
