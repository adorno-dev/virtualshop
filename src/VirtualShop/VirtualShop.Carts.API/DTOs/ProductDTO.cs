namespace VirtualShop.Carts.API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }        
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public long Stock { get; set; }
        public string ImageURL { get; set; } = "";
        public string CategoryName { get; set; } = "";
    }
}