using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.AddRequest;

namespace WalkApp.API.WalkApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        //Registering new request
        //POST: api/auth/register
        [HttpPost]
        [Route("new_user_registration")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this user 
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User is Registered Succefully!!");
                    }
                }
            }
            return BadRequest("Something went wrong!!");
        }

        //Login request
        //POST: api/auth/login
        [HttpPost]
        [Route("registered_user_login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null)
            {
                var CheckPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (CheckPasswordResult)
                {
                    // Create Token
                    return Ok();
                }
            }
            return BadRequest("Incorrect username or password");
        }
    }
}
