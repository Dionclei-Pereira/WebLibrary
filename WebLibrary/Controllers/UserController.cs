using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Data;
using WebLibrary.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Controllers {
    [ApiController]
    [Route("/users")]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;
        public UserController(SeedDB seeding, IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Index() {
            return Ok(_userService.GetUsersDTO());
        }
    }
}
