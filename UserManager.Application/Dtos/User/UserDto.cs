using UserManager.Application.Dtos.Role;

namespace UserManager.Application.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<RoleDto> UserRoles { get; set; }
    }
}
