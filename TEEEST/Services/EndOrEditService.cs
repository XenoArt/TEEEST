// EndOrEditService.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TEEEST.Data;
using TEEEST.Services;

public class EndOrEditService : IEndOrEditService // Ensure this properly implements the interface
{
    private readonly AppDbContext _context;

    public EndOrEditService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EndOrEdit>> GetAllAsync()
    {
        return await _context.EndOrEdits.ToListAsync();
    }

    public async Task<EndOrEdit> GetByIdAsync(int id)
    {
        return await _context.EndOrEdits.FindAsync(id);
    }

    public async Task<EndOrEdit> CreateAsync(EndOrEdit entity)
    {
        entity.Id = 0; // Ensure EF Core auto-generates the ID
        _context.EndOrEdits.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(EndOrEdit entity)
    {
        _context.EndOrEdits.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.EndOrEdits.FindAsync(id);
        if (entity == null) return false;

        _context.EndOrEdits.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
