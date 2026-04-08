using Microsoft.AspNetCore.Mvc;
using CepSystem.Application.Interfaces;
using CepSystem.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace CepSystem.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfileAsync()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(userIdClaim);

            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found " });
            }

            return Ok(user);
        }

        [HttpPost]
        [EnableRateLimiting("register")]

        public async Task<IActionResult> AddUserAsync(CreateUserDto userDto)
        {

            var user = await _userService.AddUserAsync(userDto);

            if (user == null)
            {
                return BadRequest(new { message = "Error in registered " });
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut]

        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto userDto)
        {

            var user = await _userService.UpdateUserAsync(userDto);

            if (!user)
            {
                return NotFound(new { message = "User not found " });
            }

            return Ok(new { message = "User updated successfuly" });
        }

        [Authorize]
        [HttpDelete]

        public async Task<IActionResult> DeleteUserAsync()
        {

            var userClaimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userId = Guid.Parse(userClaimId);

            var user = await _userService.DeleteUserAsync(userId);

            if (user == false)
            {

                return NotFound(new { message = "User not found or id invalid " });
            }

            return Ok(new { message = "User deleted successfuly " });
        }
    }
}
