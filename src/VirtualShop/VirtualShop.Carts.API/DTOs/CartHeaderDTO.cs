using System.ComponentModel.DataAnnotations;

namespace VirtualShop.Carts.API.DTOs
{
    public class CartHeaderDTO
    {
        public int Id { get; set; }        

        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; } = "";
        public string CouponCode { get; set; } = "";
    }
}