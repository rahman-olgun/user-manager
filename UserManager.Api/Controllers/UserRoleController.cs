using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManager.Application.Dtos.UserRole;
using UserManager.Application.Interfaces;

namespace UserManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize( Roles = "Admin")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRolesService)
        {
            _userRoleService = userRolesService;
        }

        [HttpPost("AssignRoleToUser")]
        [SwaggerOperation(Summary = "Assign Role to User the better way")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAssignmentAsync(CreateUserRoleDto createUserRoleDto)
        {
            try
            {
                var result = await _userRoleService.CreateAssignmentAsync(createUserRoleDto);

                if (result)
                {
                    return Ok("Role successfully assigned!");
                }
                else
                {
                    return BadRequest("Assigning the role failed!");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("RemoveRoleFromUser")]
        [SwaggerOperation(Summary = "Remove Role from User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveRoleFromUser(CreateUserRoleDto createUserRoleDto)
        {
            try
            {
                var result = await _userRoleService.RemoveRoleFromUserAsync(createUserRoleDto);

                if (result)
                {
                    return Ok("Role successfully removed!");
                }
                else
                {
                    return BadRequest("Removing the role failed!");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
