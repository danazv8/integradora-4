using Integradora4.Entities;
using Integradora4.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Integradora4;

namespace Integradora4.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly Integradora4Context _context;

        public ProductRepository(Integradora4Context context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products
                .FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;

                _context.SaveChanges();
            }
        }

        public void DeleteProduct(Product product)
        {
            var existingProduct = _context.Products
                .FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                _context.Products.Remove(existingProduct);
                _context.SaveChanges();
            }
        }

        public List<Product> SearchProductsByName(string name)
        {
            return _context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }
    }
}



