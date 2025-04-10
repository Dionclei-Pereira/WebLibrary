using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Data;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Exceptions;
using WebLibrary.Services.Interfaces;
using WebLibrary.Services.Util;

namespace WebLibrary.Controllers {
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public UserController(SeedDB seeding, IUserService userService, UserManager<User> userManager) {
            _userManager = userManager;
            _userService = userService;

        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers() {
            return Ok(await _userService.GetUsersDTO());
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<ActionResult> GetUserByEmail(string email) {
            try {
                var user = await _userService.GetUserByEmail(email);
                return Ok(user);
            } catch (UserException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{email}/loans")]
        public async Task<ActionResult> GetUserLoans(string email) {
            try {
                var loans = await _userService.GetUserLoansByEmail(email);
                return Ok(loans);
            } catch (UserException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AuthRequired("Admin")]
        public async Task<ActionResult> Insert([FromBody] UserDetails request) {
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
            UserDTO user = await _userService.GetUserByEmail(request.Email);
            var uri = Url.Action(nameof(GetUserByEmail), new { email = user.Email });
            return Created(uri, user);
        }

        [HttpPut]
        [Route("{Email}")]
        [AuthRequired("Admin")]
        public async Task<ActionResult> Update(string Email, [FromBody] UserDetails request) {
            try {
                if (request == null) {
                    return BadRequest();
                }
                var user = await _userManager.FindByEmailAsync(Email);
                if (await _userManager.FindByEmailAsync(request.Email) != null && request.Email != user.Email) {
                    return BadRequest();
                }
                user.Email = request.Email ?? user.Email;
                user.UserName = request.Email ?? user.UserName;
                user.Name = request.Name ?? user.Name;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) {
                    return BadRequest(result.Errors);
                }
                var updatedUser = await _userManager.FindByEmailAsync(request.Email);
                var uri = Url.Action(nameof(GetUserByEmail), new { email = updatedUser.Email });
                return Created(uri, updatedUser.ToDTO());
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}
