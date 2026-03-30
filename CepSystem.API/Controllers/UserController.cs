using Microsoft.AspNetCore.Mvc;
using CepSystem.Application.Interfaces;
using CepSystem.Application.Dtos;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id)
        {

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found " });
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(CreateUserDto userDto)
        {

            var user = await _userService.AddUserAsync(userDto);

            if (user == null)
            {
                return BadRequest(new { message = "Error in registered " });
            }

            return Ok(user);
        }

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

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        {

            var user = await _userService.DeleteUserAsync(id);

            if (user == false)
            {

                return NotFound(new { message = "User not found or id invalid " });
            }

            return Ok(new { message = "User deleted successfuly " });
        }
    }
}
