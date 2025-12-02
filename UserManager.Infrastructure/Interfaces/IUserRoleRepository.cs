using UserManager.Domain.Entities;

namespace UserManager.Infrastructure.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<bool> CreateAssignmentAsync(UserRole userRole);

        Task<bool> DeleteAssignmentAsync(UserRole userRole);
    }
}
