namespace UserManager.Application.Dtos.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}
