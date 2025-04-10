using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase {

        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public AuthController(ITokenService tokenService, UserManager<User> userManager) {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] RequestLogin request) {
            if (request.Equals == null || request.password == null) return BadRequest("Email or password is invalid");
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.password)) return BadRequest("Email or password is invalid");
            var token = await _tokenService.GenerateToken(user);
            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] UserDetails request) {
            if (request == null || request.Email == null || request.Password == null || request.Password == null) {
                return BadRequest();
            }
            User u = new User();
            u.Email = request.Email;
            u.UserName = request.Email;
            u.Name = request.Name;
            var result = await _userManager.CreateAsync(u, request.Password);
            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }
            return Ok();
        }
    }
}
