using Microsoft.EntityFrameworkCore;
using UserManager.Domain.Entities;
using UserManager.Infrastructure.Interfaces;

namespace UserManager.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Name == name);
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            var results = await _context.SaveChangesAsync();
            return results > 0;
        }

        public async Task<bool> DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
