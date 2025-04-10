using Microsoft.AspNetCore.Mvc;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Exceptions;
using WebLibrary.Services.Interfaces;
using WebLibrary.Services.Util;

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
        [AuthRequired("Admin")]
        public async Task<ActionResult> GetPenalties() {
            var loans = (await _loanService.GetLoans()).Where(l => l.DateBack < DateTime.Now.ToUniversalTime()).ToList();
            return Ok(loans);
        }

        [HttpGet]
        [Route("{email}/penalty")]
        [AuthRequired("Admin")]
        public async Task<ActionResult> GetUserPenality(string email) {
            try {
                var user = await _userService.GetUserByEmail(email);
                return Ok(user.Penalty);
            } catch (UserException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("{email}/penalty")]
        [AuthRequired("Admin")]
        public async Task<ActionResult> AddPenalty(string email, [FromBody] PenaltyRequest penalty) {
            try {
                var user = await _userService.GetUserByEmail(email);
                double amount = await _userService.AddPenalty(email, penalty.Amount);
                return Ok(amount);
            } catch (UserException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{email}/penalty")]
        [AuthRequired("Admin")]
        public async Task<ActionResult> ResetPenalty(string email) {
            try {
                var user = await _userService.GetUserByEmail(email);
                double amount = await _userService.ResetPenalty(email);
                return Ok(amount);
            } catch (UserException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{email}/penalty")]
        [AuthRequired("Admin")]
        public async Task<ActionResult> SetPenalty(string email, [FromBody] PenaltyRequest penalty) {
            try {
                var user = await _userService.GetUserByEmail(email);
                double amount = await _userService.SetPenalty(email, penalty.Amount);
                return Ok(amount);
            } catch (UserException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
