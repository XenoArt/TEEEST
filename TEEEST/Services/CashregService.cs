using Microsoft.EntityFrameworkCore;
using TEEEST.Data;
using TEEEST.Models;

namespace TEEEST.Services
{
    public class CashregService : ICashregService
    {
        private readonly AppDbContext _context;

        public CashregService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Cashreg> GetCurrentRegister()
        {
            return await _context.CashRegisters.FirstOrDefaultAsync()
                ?? throw new InvalidOperationException("Cash register not found");
        }

        public async Task<Cashreg> UpdateRegister(decimal cash, decimal card)
        {
            var register = await GetCurrentRegister();
            register.Cash += cash;
            register.Card += card;
            register.Total = register.Cash + register.Card;

            _context.CashRegisters.Update(register);
            await _context.SaveChangesAsync();
            return register;
        }

        public async Task<Cashreg> ResetAndAddCash(decimal amount)
        {
            var register = await GetCurrentRegister();
            register.Cash = amount;
            register.Card = 0;
            register.Total = amount;

            _context.CashRegisters.Update(register);
            await _context.SaveChangesAsync();
            return register;
        }

        public async Task<Cashreg> UpdtCard(decimal amount)
        {
            var register = await GetCurrentRegister();
            register.Card = amount;
            register.Total = register.Cash + register.Card;
            _context.CashRegisters.Update(register);
            await _context.SaveChangesAsync();
            return register;
        }

        public async Task<Cashreg> UpdtCash(decimal amount)
        {
            var register = await GetCurrentRegister();
            register.Cash = amount;
            register.Total = register.Cash + register.Card;
            _context.CashRegisters.Update(register);
            await _context.SaveChangesAsync();
            return register;
        }
    }
}
