using TEEEST.Models;

public interface IPurchaseRepository
{
    Task AddPurchaseAsync(PurchaseRecord purchase);
    Task RemovePurchaseAsync(int id);
    Task<IEnumerable<PurchaseRecord>> GetAllPurchasesAsync();
}