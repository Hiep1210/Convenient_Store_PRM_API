namespace ConvenientStoreAPI.Mapper.DTO
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SupplierId { get; set; }
        public double? Price { get; set; }
        public int CatId { get; set; }
        public int? ImageId { get; set; }
    }
}
