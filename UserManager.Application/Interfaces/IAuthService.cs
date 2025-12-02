using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace UserManager.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal> SignInAsync(string employeeId, string password, HttpContext httpContext);

        Task SignOutAsync(HttpContext httpContext);
    }
}
