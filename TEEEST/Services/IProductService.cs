using TEEEST.Models;

namespace TEEEST.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> GetProductByNameAsync(string productName);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(string productName);
        Task<Product> AdjustStockAsync(string productName, int quantityAdjustment);
        Task<decimal> GetProductPriceAsync(string productName);
        Task<Product> DecreaseProductQuantityAsync(string productName, int quantityToDecrease);
    }
}
