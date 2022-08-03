namespace VirtualShop.Carts.API.Models
{
    public class Product
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