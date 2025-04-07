using TEEEST.Models;

namespace TEEEST.Services
{
    public interface IActiveService
    {
        Task<IEnumerable<Active>> GetAllActivesAsync();
        Task<Active> GetActiveByIdAsync(int id);
        Task<Active> CreateActiveAsync(Active active);
        Task UpdateActiveAsync(int id, Active active);
        Task DeleteActiveAsync(int id);
    }
}
