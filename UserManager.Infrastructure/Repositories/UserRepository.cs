using Microsoft.EntityFrameworkCore;
using UserManager.Domain.Entities;
using UserManager.Infrastructure.Interfaces;

namespace UserManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool includeRoles = true)
        {
            var query = _context.Users.AsNoTracking();

            if (includeRoles)
                query = query.Include(user => user.UserRoles)!.ThenInclude(ur => ur.Role);

            return await query.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id, bool includeRoles = true)
        {
            var query = _context.Users.AsNoTracking();

            if (includeRoles)
                query = query.Include(user => user.UserRoles)!.ThenInclude(ur => ur.Role);

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmployeeIdAsync(string employeeId, bool includeRoles = true)
        {
            var query = _context.Users.AsNoTracking();

            if (includeRoles)
                query = query.Include(user => user.UserRoles)!.ThenInclude(ur => ur.Role);

            return await query.FirstOrDefaultAsync(u => u.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<User>> GetByRoleAsync(string role)
        {
            var query = _context.Users
                .Include(u => u.UserRoles)!
                .ThenInclude(ur => ur.Role)
                .Where(ur => ur.UserRoles!.Any(r => r.Role!.Name == role));

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            var results = await _context.SaveChangesAsync();
            return results > 0;
        }

        public async Task<bool> DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
