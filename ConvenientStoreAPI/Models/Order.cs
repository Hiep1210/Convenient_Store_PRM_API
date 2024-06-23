using System;
using System.Collections.Generic;

namespace ConvenientStoreAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            Orderdetails = new HashSet<Orderdetail>();
        }

        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
    }
}
