using Microsoft.AspNetCore.Mvc;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Controllers {

    [ApiController]
    [Route("api/penalties")]
    public class PenaltyController : ControllerBase {

        private readonly IUserService _userService;
        private readonly ILoanService _loanService;

        public PenaltyController(IUserService userService, ILoanService loanService) {
            _userService = userService;
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPenalties() {
            var loans = (await _loanService.GetLoans()).Where(l => l.DateBack < DateTime.Now.ToUniversalTime()).ToList();
            return Ok(loans);
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
    }
}
