using System;
using System.Collections.Generic;

namespace ConvenientStoreAPI.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public byte[] Image1 { get; set; } = null!;
    }
}
