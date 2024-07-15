using ConvenientStoreAPI.Models;

namespace ConvenientStoreAPI.Mapper.DTO
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; } = null!;
        public bool IsProcess { get; set; }
        public ICollection<OrderDetailRequest> Orderdetails { get; set; }
    }
}
