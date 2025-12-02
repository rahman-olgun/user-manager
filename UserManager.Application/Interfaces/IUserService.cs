using UserManager.Application.Dtos.User;
using UserManager.Domain.Entities;

namespace UserManager.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> AddUserAsync(CreateUserDto createUserDto);

        Task<IEnumerable<UserDto>> GetAllUsersAsync(bool includeRoles = true);

        Task<UserDto> GetUserByIdAsync(int id, bool includeRoles = true);

        Task<UserDto> GetUserByEmployeeIdAsync(string employeeId, bool includeRoles = true);

        Task<IEnumerable<UserDto>> GetUsersByRoleAsync(string role);

        Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto);

        Task<bool> DeleteUserAsync(int id);
    }
}
