using UserManager.Domain.Entities;

namespace UserManager.Infrastructure.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> AddAsync(Role role);

        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int id);

        Task<Role?> GetByNameAsync(string name);

        Task<bool> UpdateAsync(Role role);

        Task<bool> DeleteAsync(Role role);
    }
}
