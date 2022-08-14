namespace VirtualShop.Web.Models
{
    public class CouponViewModel
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public string? CouponCode { get; set; }
    }
}