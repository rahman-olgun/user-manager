using UserManager.Application.Dtos.Role;
using UserManager.Domain.Entities;

namespace UserManager.Application.Interfaces
{
    public interface IRoleService
    {
        Task<Role?> AddRoleAsync(CreateRoleDto createRoleDto);

        Task<IEnumerable<RoleDto>> GetAllRolesAsync();

        Task<RoleDto> GetRoleByIdAsync(int id);

        Task<RoleDto> GetRoleByNameAsync(string name);

        Task<bool> UpdateRoleAsync(UpdateRoleDto updateRoleDto);

        Task<bool> DeleteRoleAsync(int id);
    }
}
