using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Services.Interfaces;

namespace WebLibrary.Controllers {
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService) {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks() {
            return Ok(await _bookService.GetBooks());
        }
    }
}
