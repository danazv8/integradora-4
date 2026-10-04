using Microsoft.AspNetCore.Mvc;
using Integradora4.Services.Interfaces;
using Integradora4.Models.DTOs.Requests;

namespace Integradora4.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("search")]
        public IActionResult Search(string? name)
        {
            var products = _service.SearchProducts(name);
            return Ok(products);
        }
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var stats = _service.GetStats();
            return Ok(stats);
        }
      
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductForCreateDto dto)
        {
            try
            {
                var product = _service.CreateProduct(dto);
                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = product.Id },
                    product);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _service.UpdateProduct(id, dto);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _service.DeleteProduct(id);
            return NoContent();
        }
    }
}
