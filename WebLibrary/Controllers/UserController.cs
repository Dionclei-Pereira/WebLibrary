using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Data;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Interfaces;

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
            var user = await _userService.GetUserByEmail(email);
            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("{email}/penalty")]
        public async Task<ActionResult> GetUserPenality(string email) {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) {
                return NotFound();
            }
            return Ok(user.Penalty);
        }

        [HttpPost]
        [Route("{email}/penalty")]
        public async Task<ActionResult> AddPenalty(string email, [FromBody] PenaltyRequest penalty) {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) {
                return NotFound();
            }
            double amount = await _userService.AddPenalty(email, penalty.Amount);
            return Ok(amount);
        }

        [HttpDelete]
        [Route("{email}/penalty")]
        public async Task<ActionResult> ResetPenalty(string email) {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) {
                return NotFound();
            }
            double amount = await _userService.ResetPenalty(email);
            return Ok(amount);
        }

        [HttpPut]
        [Route("{email}/penalty")]
        public async Task<ActionResult> SetPenalty(string email, [FromBody] PenaltyRequest penalty) {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) {
                return NotFound();
            }
            double amount = await _userService.SetPenalty(email, penalty.Amount);
            return Ok(amount);
        }

        [HttpGet]
        [Route("{email}/loans")]
        public async Task<ActionResult> GetUserLoans(string email) {
            var loans = await _userService.GetUserLoansByEmail(email);
            return Ok(loans);
        }

        [HttpPost]
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
        public async Task<ActionResult> Update(string Email, [FromBody] UserDetails request) {
            if (request == null) {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null) {
                return NotFound();
            }
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
        }
    }
}
