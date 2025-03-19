using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Entities;
using WebLibrary.Entities.DTO;
using WebLibrary.Services.Exceptions;
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetBookById(int id) {
            try {
                var resultBook = await _bookService.GetBookById(id);
                if (resultBook == null) {
                    return NotFound();
                }
                return Ok(resultBook);
            } catch (BookException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] BookDetails bookDetails) {
            Book book = new Book();
            book.Author = bookDetails.Author;
            book.Name = bookDetails.Name;
            BookDTO resultBook = await _bookService.Insert(book);
            if (resultBook != null) {
                var uri = Url.Action(nameof(GetBookById), new { id = resultBook.Id });
                return Created(uri, resultBook);
            }
            return BadRequest();
        }
    }
}
