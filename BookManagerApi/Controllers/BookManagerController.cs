using Microsoft.AspNetCore.Mvc;
using BookManagerApi.Models;
using BookManagerApi.Services;

namespace BookManagerApi.Controllers
{
    [Route("api/v1/book")]
    [ApiController]
    public class BookManagerController : ControllerBase
    {
        private readonly IBookManagementService _bookManagementService;

        public BookManagerController(IBookManagementService bookManagementService)
        {
            _bookManagementService = bookManagementService;
        }

        // GET: api/v1/book
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = _bookManagementService.GetAllBooks();
            if (books == null || books.Count() == 0)
                return NotFound("No books are currently present in the collection.");
            return books;
        }

        // GET: api/v1/book/5
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(long id)
        {
            Book book = null;
            try
            {
                book = _bookManagementService.FindBookById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            if(book == null)
                return NotFound("The given book id does not exist. Please make sure you have the correct id and try again.");

            return book;
        }

        // PUT: api/v1/book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateBookById(long id, Book book)
        {
            try
            {
                _bookManagementService.Update(id, book);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
            
        }

        // POST: api/v1/book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            try
            {
                _bookManagementService.Create(book);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBookById(long id)
        {
            _bookManagementService.DeleteBookById(id);
            return NoContent();
        }
    }
}
