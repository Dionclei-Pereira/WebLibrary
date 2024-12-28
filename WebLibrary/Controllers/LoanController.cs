using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Controllers {
    [Route("api/loans")]
    [ApiController]
    public class LoanController : ControllerBase {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService) {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult> GetLoans() {
            return Ok(await _loanService.GetLoans());
        }

    }
}
