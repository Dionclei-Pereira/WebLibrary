using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Data;
using WebLibrary.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Controllers {
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;
        public UserController(SeedDB seeding, IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers() {
            return Ok(await _userService.GetUsersDTO());
        }

        [HttpGet]
        [Route("/{email}")]
        public async Task<ActionResult> GetUserByEmail(string email) {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("/{email}/loans")]
        public async Task<ActionResult> GetUserLoans(string email) {
            var loans = await _userService.GetUserLoansByEmail(email);
            return Ok(loans);
        }
    }
}
