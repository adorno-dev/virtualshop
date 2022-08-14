namespace VirtualShop.Web.Models
{
    public class CartHeaderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";

        public string CouponCode { get; set; } = "";
        public decimal TotalAmount { get; set; } = 0.00M;

        public decimal Discount { get; set; } = 0.00M;
    }
}