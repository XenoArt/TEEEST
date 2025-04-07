using Microsoft.EntityFrameworkCore;
using TEEEST.Data;
using TEEEST.Models;

namespace TEEEST.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetProductByNameAsync(string productName)
        {
            return await _context.Products.FindAsync(productName);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(string productName)
        {
            var product = await _context.Products.FindAsync(productName);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> AdjustStockAsync(string productName, int quantityAdjustment)
        {
            var product = await _context.Products.FindAsync(productName);
            if (product == null)
                throw new KeyNotFoundException($"Product '{productName}' not found");

            if (quantityAdjustment < 0 && product.StockQuantity < Math.Abs(quantityAdjustment))
                throw new InvalidOperationException(
                    $"Cannot remove {Math.Abs(quantityAdjustment)} items. Only {product.StockQuantity} available.");

            product.StockQuantity += quantityAdjustment;
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> DecreaseStock(string productName, int QuantityToRemove)
        {
            var product = await _context.Products.FindAsync(productName);
            if (product == null)
                throw new KeyNotFoundException($"Product '{productName}' not found");

            if (QuantityToRemove < 0 && product.StockQuantity < Math.Abs(QuantityToRemove))
                throw new InvalidOperationException(
                    $"Cannot remove {Math.Abs(QuantityToRemove)} items. Only {product.StockQuantity} available.");

            product.StockQuantity -= QuantityToRemove;
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<decimal> GetProductPriceAsync(string productName)
        {
            var product = await GetProductByNameAsync(productName);
            return product?.Price ?? 0;
        }
        public async Task<Product> DecreaseProductQuantityAsync(string productName, int quantityToDecrease)
        {
            if (quantityToDecrease <= 0)
                throw new ArgumentException("Quantity to decrease must be positive", nameof(quantityToDecrease));

            var product = await _context.Products.FindAsync(productName);
            if (product == null)
                throw new KeyNotFoundException($"Product '{productName}' not found");

            if (product.StockQuantity < quantityToDecrease)
                throw new InvalidOperationException(
                    $"Cannot decrease {quantityToDecrease} items. Only {product.StockQuantity} available.");

            product.StockQuantity -= quantityToDecrease;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
