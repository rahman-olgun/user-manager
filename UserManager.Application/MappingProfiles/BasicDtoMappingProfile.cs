using AutoMapper;
using UserManager.Application.Dtos.Role;
using UserManager.Application.Dtos.User;
using UserManager.Application.Dtos.UserRole;
using UserManager.Domain.Entities;

namespace UserManager.Application.MappingProfiles
{
    public class BasicDtoMappingProfile : Profile
    {
        public BasicDtoMappingProfile()
        {
            // User
            CreateMap<User, CreateUserDto>()
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.PasswordHash = BCrypt.Net.BCrypt.HashPassword(src.Password);
                });

            CreateMap<User, UpdateUserDto>()
                .ReverseMap()
                .AfterMap((src, dest) =>
                {
                    dest.PasswordHash = BCrypt.Net.BCrypt.HashPassword(src.Password);
                });

            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.UserRoles,
                    opt => opt.MapFrom(src =>
                        src.UserRoles!
                            .Select(ur => new RoleDto
                            {
                                Id = ur.Role!.Id,
                                Name = ur.Role.Name
                            }).ToList()));

            // Role
            CreateMap<Role, CreateRoleDto>().ReverseMap();
            CreateMap<Role, UpdateRoleDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();

            // UserRole
            CreateMap<UserRole, CreateUserRoleDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
        }
    }
}
