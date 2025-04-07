namespace TEEEST.Models
{
    public class Product
    {
        public required string Name { get; set; }  // Added 'required' modifier
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
