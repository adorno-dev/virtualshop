namespace VirtualShop.Carts.API.DTOs
{
    public class CheckoutHeaderDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";

        public string CouponCode { get; set; } = "";
        public decimal TotalAmount { get; set; } = 0.00M;

        public decimal Discount { get; set; } = 0.00M;


        public DateTime DateTime { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Telephone { get; set; } = "";
        public string Email { get; set; } = "";

        public string CardNumber { get; set; } = "";
        public string NameOnCard { get; set; } = "";
        public string CVV { get; set; } = "";
        public string ExpireMonthYear { get; set; } = "";

        public IEnumerable<CartItemDTO>? CartItems { get; set; }
        public int CartTotalItems { get; set; }
    }
}