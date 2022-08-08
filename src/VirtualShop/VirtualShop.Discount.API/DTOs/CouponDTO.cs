using System.ComponentModel.DataAnnotations;

namespace VirtualShop.Discount.API.DTOs
{
    public class CouponDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string? CouponCode { get; set; }

        [Required]
        public decimal Discount { get; set; }
    }
}