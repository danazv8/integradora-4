namespace Integradora4.Models.DTOs.Responses
{
    public class ProductStatsDto
    {
        public int TotalProducts { get; set; }
        public decimal AveragePrice { get; set; }
        public string MostExpensiveName { get; set; } = string.Empty;
    }
}
