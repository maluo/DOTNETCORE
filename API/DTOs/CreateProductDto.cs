using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public required string Name { get; set; } =  string.Empty;
        public string Description { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        [Required]
        public required string Type { get; set; }
        [Required]
        public required string Brand { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be at least 1.")]
        public int QuantityInStock { get; set; }
    }
}