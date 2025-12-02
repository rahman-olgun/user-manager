using AutoMapper;
using UserManager.Application.Dtos.Role;
using UserManager.Application.Interfaces;
using UserManager.Domain.Entities;
using UserManager.Infrastructure.Interfaces;

namespace UserManager.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<Role?> AddRoleAsync(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<Role>(createRoleDto);

            var created = await _roleRepository.AddAsync(role);

            return created;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            if (roles == null)
            {
                throw new KeyNotFoundException("No roles found!");
            }

            var readDto = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return readDto;
        }

        public async Task<RoleDto> GetRoleByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found!");
            }

            var readDto = _mapper.Map<RoleDto>(role);
            return readDto;
        }

        public async Task<RoleDto> GetRoleByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid Name!");
            }

            var role = await _roleRepository.GetByNameAsync(name);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found!");
            }

            var readDto = _mapper.Map<RoleDto>(role);

            return readDto;
        }

        public async Task<bool> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
        {
            if (updateRoleDto.Id <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var role = _mapper.Map<Role>(updateRoleDto);

            if (role == null)
            {
                throw new KeyNotFoundException("Keine Rollen gefunden!");
            }

            bool isUpdated = await _roleRepository.UpdateAsync(role);

            return isUpdated;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found!");
            }

            bool isDeleted = await _roleRepository.DeleteAsync(role);

            return isDeleted;
        }
    }
}
