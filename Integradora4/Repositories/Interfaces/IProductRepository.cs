using Integradora4.Entities;

namespace Integradora4.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product? GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

    }
}
