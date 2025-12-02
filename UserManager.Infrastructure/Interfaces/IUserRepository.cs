using UserManager.Domain.Entities;

namespace UserManager.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> AddAsync(User user);

        Task<IEnumerable<User>> GetAllAsync(bool includeRoles = true);

        Task<User?> GetByIdAsync(int id, bool includeRoles = true);

        Task<User?> GetByEmployeeIdAsync(string employeeId, bool includeRoles = true);

        Task<IEnumerable<User>> GetByRoleAsync(string role);

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(User user);
    }
}
