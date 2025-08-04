using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.AddRequest;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.New;

namespace WalkApp.API.WalkApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        //Registering new request
        //POST: https://localhost:7204/api/auth/new_user_registration
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
        //POST: https://localhost:7204/api/auth/registered_user_login
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
                    //Get roles for this user
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token
                        var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Incorrect username or password");
        }
    }
}
