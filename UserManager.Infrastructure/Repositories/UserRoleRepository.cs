using UserManager.Domain.Entities;
using UserManager.Infrastructure.Interfaces;

namespace UserManager.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAssignmentAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            var results = await _context.SaveChangesAsync();
            return results > 0;
        }

        public async Task<bool> DeleteAssignmentAsync(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
