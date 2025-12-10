using Microsoft.EntityFrameworkCore;
using TalentoPlus.Domain.Entities;
using TalentoPlus.Domain.Interfaces;
using TalentoPlus.Infrastructure.Data;

namespace TalentoPlus.Infrastructure.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Employee>> GetAllAsync()
    {
        // Eager load Department
        return await _dbSet.Include(e => e.Department).ToListAsync();
    }

    public override async Task<Employee?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return await _dbSet.Include(e => e.Department)
                           .FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentAsync(int departmentId)
    {
        return await _dbSet.Include(e => e.Department)
                           .Where(e => e.DepartmentId == departmentId)
                           .ToListAsync();
    }
}
