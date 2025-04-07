using Microsoft.EntityFrameworkCore;
using TEEEST.Data;
using TEEEST.Models;

namespace TEEEST.Services
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;

        public PurchaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPurchaseAsync(PurchaseRecord purchase)
        {
            // Add the PurchaseRecord, EF Core will handle auto-increment of the Id
            await _context.PurchaseRecords.AddAsync(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePurchaseAsync(int id)
        {
            var purchase = await _context.PurchaseRecords.FindAsync(id);
            if (purchase != null)
            {
                _context.PurchaseRecords.Remove(purchase);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PurchaseRecord>> GetAllPurchasesAsync()
        {
            return await _context.PurchaseRecords.ToListAsync();
        }
    } 
}
