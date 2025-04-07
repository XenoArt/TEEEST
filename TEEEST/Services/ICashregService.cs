using TEEEST.Models;

namespace TEEEST.Services
{
    public interface ICashregService
    {
        Task<Cashreg> GetCurrentRegister();
        Task<Cashreg> UpdateRegister(decimal cash, decimal card);
        Task<Cashreg> ResetAndAddCash(decimal amount);
        Task<Cashreg> UpdtCard(decimal amount);
        Task<Cashreg> UpdtCash(decimal amount);
    }
}
