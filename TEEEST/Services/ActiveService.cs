using Microsoft.EntityFrameworkCore;
using TEEEST.Data;
using TEEEST.Models;

namespace TEEEST.Services
{
    public class ActiveService : IActiveService
    {
        private readonly AppDbContext _context;

        public ActiveService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Active>> GetAllActivesAsync()
        {
            return await _context.Actives.ToListAsync();
        }

        public async Task<Active> GetActiveByIdAsync(int id)
        {
            return await _context.Actives.FindAsync(id);
        }

        public async Task<Active> CreateActiveAsync(Active active)
        {
            _context.Actives.Add(active);
            await _context.SaveChangesAsync();
            return active;
        }

        public async Task UpdateActiveAsync(int id, Active active)
        {
            if (id != active.Id)
            {
                throw new System.ArgumentException("ID mismatch");
            }

            _context.Entry(active).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteActiveAsync(int id)
        {
            var active = await _context.Actives.FindAsync(id);
            if (active != null)
            {
                _context.Actives.Remove(active);
                await _context.SaveChangesAsync();
            }
        }
    }

}