using Integradora4.Entities;
using Integradora4.Models.DTOs.Requests;
using Integradora4.Models.DTOs.Responses;
using Integradora4.Services.Interfaces;
using Integradora4.Repositories.Interfaces;


namespace Integradora4.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public List<ProductForReadDto> GetAllProducts()
        {
            var products = _repository.GetAllProducts();
            return products.Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public ProductForReadDto? GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null) return null;

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
        
        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            var existingProduct = _repository.GetAllProducts()
                .FirstOrDefault(p => p.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));

            if (existingProduct != null)
            {
                throw new InvalidOperationException("Ya existe un producto con ese nombre.");
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.AddProduct(product);

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
            {
                return;
            }
            product.Name = dto.Name;
            product.Price = dto.Price;

            _repository.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
            {
                return;
            }
            _repository.DeleteProduct(product);
        }
        public List<ProductForReadDto> SearchProducts(string? name)
        {
            var products = _repository.GetAllProducts();
            if (string.IsNullOrWhiteSpace(name))
            {

                return products.Select(p => new ProductForReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList();
            }
            return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Select(p => new ProductForReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList();

        }
        public ProductStatsDto GetStats()
        {
            var products = _repository.GetAllProducts();
            if (products.Count == 0)
            {
                return new ProductStatsDto
                {
                    TotalProducts = 0,
                    AveragePrice = 0,
                    MostExpensiveName = string.Empty
                };
            }
            var mostExpensive = products.OrderByDescending(p => p.Price).First();

            return new ProductStatsDto
            {
                TotalProducts = products.Count,
                AveragePrice = products.Average(p => p.Price),
                MostExpensiveName = mostExpensive.Name
            };
        }
    }
}
