using AutoMapper;
using UserManager.Application.Dtos.User;
using UserManager.Application.Interfaces;
using UserManager.Domain.Entities;
using UserManager.Infrastructure.Interfaces;

namespace UserManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User?> AddUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            if (!string.IsNullOrEmpty(createUserDto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
            }

            var created = await _userRepository.AddAsync(user);

            return created;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(bool includeRoles = true)
        {
            var users = await _userRepository.GetAllAsync(includeRoles);

            if (users == null)
            {
                throw new KeyNotFoundException("No users found!");
            }

            var readDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return readDto;
        }

        public async Task<UserDto> GetUserByIdAsync(int id, bool includeRoles = true)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var user = await _userRepository.GetByIdAsync(id, includeRoles);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            var readDto = _mapper.Map<UserDto>(user);

            return readDto;
        }

        public async Task<UserDto> GetUserByEmployeeIdAsync(string employeeId, bool includeRoles = true)
        {
            if (string.IsNullOrEmpty(employeeId))
            {
                throw new ArgumentException("Invalid Username!");
            }

            var user = await _userRepository.GetByEmployeeIdAsync(employeeId, includeRoles);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            var readDto = _mapper.Map<UserDto>(user);

            return readDto;
        }

        public async Task<IEnumerable<UserDto>> GetUsersByRoleAsync(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException("Invalid Role!");
            }

            var users = await _userRepository.GetByRoleAsync(role);

            if (users == null)
            {
                throw new KeyNotFoundException("No users assigned to this role were found!");
            }

            var readDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return readDto;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            if (updateUserDto.Id <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var user = await _userRepository.GetByIdAsync(updateUserDto.Id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            _mapper.Map(updateUserDto, user);

            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
            }

            bool isUpdated = await _userRepository.UpdateAsync(user);

            return isUpdated;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            bool isDeleted = await _userRepository.DeleteAsync(user);

            return isDeleted;
        }
    }
}
