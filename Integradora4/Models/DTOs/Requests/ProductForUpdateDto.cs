using System.ComponentModel.DataAnnotations;

namespace Integradora4.Models.DTOs.Requests
{
    public class ProductForUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
