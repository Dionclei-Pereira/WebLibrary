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
        public ActionResult GetBooks() {
            return Ok(_bookService.GetBooks());
        }
    }
}
