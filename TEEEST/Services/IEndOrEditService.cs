namespace TEEEST.Services
{
    public interface IEndOrEditService
    {
        Task<IEnumerable<EndOrEdit>> GetAllAsync();
        Task<EndOrEdit> GetByIdAsync(int id);
        Task<EndOrEdit> CreateAsync(EndOrEdit entity);
        Task<bool> UpdateAsync(EndOrEdit entity);
        Task<bool> DeleteAsync(int id);
    }
}


