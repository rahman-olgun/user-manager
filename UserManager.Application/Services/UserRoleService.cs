using AutoMapper;
using UserManager.Application.Dtos.UserRole;
using UserManager.Application.Interfaces;
using UserManager.Domain.Entities;
using UserManager.Infrastructure.Interfaces;

namespace UserManager.Application.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository userRoleRepository, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAssignmentAsync(CreateUserRoleDto createUserRoleDto)
        {
            if (createUserRoleDto.UserId <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var user = await _userRepository.GetByIdAsync(createUserRoleDto.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            if (createUserRoleDto.RoleId <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var role = await _roleRepository.GetByIdAsync(createUserRoleDto.RoleId);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found!");
            }

            var userRoles = _mapper.Map<UserRole>(createUserRoleDto);

            var assigned = await _userRoleRepository.CreateAssignmentAsync(userRoles);

            return assigned;
        }

        public async Task<bool> RemoveRoleFromUserAsync(CreateUserRoleDto createUserRoleDto)
        {
            if (createUserRoleDto.UserId <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var user = await _userRepository.GetByIdAsync(createUserRoleDto.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            if (createUserRoleDto.RoleId <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var role = await _roleRepository.GetByIdAsync(createUserRoleDto.RoleId);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found!");
            }

            var userRole = _mapper.Map<UserRole>(createUserRoleDto);

            if (userRole == null)
            {
                throw new KeyNotFoundException("The role is not assigned to the user!");
            }

            var result = await _userRoleRepository.DeleteAssignmentAsync(userRole);

            return result;
        }
    }
}
