using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TEEEST.Models;
using TEEEST.Services;

namespace TEEEST.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Product>> CreateNewProduct(
            [FromBody] NewProductRequest request)
        {
            var product = new Product
            {
                Name = request.ProductName,
                Price = request.Price,
                StockQuantity = request.InitialStock
            };

            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductByName),
                new { productName = createdProduct.Name }, createdProduct);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<Product>> GetProductByName(string productName)
        {
            var product = await _productService.GetProductByNameAsync(productName);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPatch("adjust-stock/{productName}")]
        public async Task<ActionResult<Product>> AdjustProductStock(
            string productName,
            [FromBody] StockAdjustmentRequest request)
        {
            try
            {
                var updatedProduct = await _productService.AdjustStockAsync(
                    productName, request.AdjustmentAmount);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product {productName} not found");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{productName}")]
        public async Task<ActionResult<Product>> UpdateProductDetails(
            string productName,
            [FromBody] ProductUpdateRequest request)
        {
            var existingProduct = await _productService.GetProductByNameAsync(productName);
            if (existingProduct == null) return NotFound();

            existingProduct.Name = request.NewName;
            existingProduct.Price = request.NewPrice;

            var updatedProduct = await _productService.UpdateProductAsync(existingProduct);
            return Ok(updatedProduct);
        }

        [HttpDelete("remove/{productName}")]
        public async Task<IActionResult> DeleteProduct(string productName)
        {
            var success = await _productService.DeleteProductAsync(productName);
            return success ? NoContent() : NotFound();
        }
        [HttpPatch("decrease-quantity/{productName}")]
        public async Task<ActionResult<Product>> DecreaseProductQuantity(
    string productName,
    [FromBody] DecreaseQuantityRequest request)
        {
            try
            {
                var updatedProduct = await _productService.DecreaseProductQuantityAsync(
                    productName,
                    request.QuantityToDecrease);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product {productName} not found");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class DecreaseQuantityRequest
        {
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
            public int QuantityToDecrease { get; set; }
        }
        public class NewProductRequest
        {
            [Required] public string ProductName { get; set; } = null!;
            [Range(0.01, 9999)] public decimal Price { get; set; }
            [Range(0, 9999)] public int InitialStock { get; set; }
        }

        public class StockAdjustmentRequest
        {
            [Required]
            public int AdjustmentAmount { get; set; }
        }

        public class ProductUpdateRequest
        {
            [Required] public string NewName { get; set; } = null!;
            [Range(0.01, 9999)] public decimal NewPrice { get; set; }
        }
    }
}
