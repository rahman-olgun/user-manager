using UserManager.Application.Dtos.UserRole;

namespace UserManager.Application.Interfaces
{
    public interface IUserRoleService
    {
        Task<bool> CreateAssignmentAsync(CreateUserRoleDto createUserRoleDto);

        Task<bool> RemoveRoleFromUserAsync(CreateUserRoleDto createUserRoleDto);
    }
}
