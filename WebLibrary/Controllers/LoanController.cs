using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services;
using WebLibrary.Services.Exceptions;
using WebLibrary.Services.Interfaces;
using WebLibrary.Services.Util;

namespace WebLibrary.Controllers {
    [Route("api/loans")]
    [ApiController]
    public class LoanController : ControllerBase {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        public LoanController(ILoanService loanService, IBookService bookService, IUserService userService) {
            _loanService = loanService;
            _userService = userService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult> GetLoans() {
            return Ok(await _loanService.GetLoans());
        }

        [HttpGet]
        [Route("{loanId}")]
        public async Task<ActionResult> GetLoanById(int loanId) {
            try {
                var loan = await _loanService.GetLoanById(loanId);
                return Ok(loan);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [AuthRequired("Admin")]
        public async Task<ActionResult> AddLoan([FromBody] LoanRequest currentRequest) {
            try {
                var user = await _userService.GetUserByEmailNoDTO(currentRequest.email);
                var book = await _bookService.GetBookByIdNoDTO(currentRequest.bookId);
                Loan loanCreated = await _loanService.AddLoan(user, book);
                var uri = Url.Action(nameof(GetLoanById), new { loanId = loanCreated.Id });
                return Created(uri, currentRequest);
            } catch (LoanException e) {
                return BadRequest(e.Message);
            } catch (BookException e) {
                return BadRequest(e.Message);
            } catch (UserException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("{loanId}/renew")]
        [AuthRequired("Admin")]
        public async Task<ActionResult> Renew(int loanId) {
            try {
                var loan = await _loanService.Renew(loanId);
                return Ok(loan);
            } catch (LoanException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [AuthRequired("Admin")]
        public async Task<ActionResult> RemoveLoan([FromBody] LoanDeleteRequest currentRequest) {
            await _loanService.RemoveLoan(currentRequest.loanId);
            return Ok();
        }
    }
}
