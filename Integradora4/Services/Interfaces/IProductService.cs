using Integradora4.Models.DTOs.Requests;
using Integradora4.Models.DTOs.Responses;

namespace Integradora4.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductForReadDto> GetAllProducts();
        ProductForReadDto? GetProductById(int id);
        ProductForReadDto CreateProduct (ProductForCreateDto dto);

        void UpdateProduct(int id, ProductForUpdateDto dto);
        void DeleteProduct(int id);
        List<ProductForReadDto> SearchProducts(string? name);

        ProductStatsDto GetStats();


    }
}
