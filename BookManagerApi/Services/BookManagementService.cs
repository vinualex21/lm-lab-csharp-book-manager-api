using BookManagerApi.Models;

namespace BookManagerApi.Services
{
	public class BookManagementService : IBookManagementService
	{
        private readonly BookContext _context;

        public BookManagementService(BookContext context)
        {
            _context = context;
        }


        public List<Book> GetAllBooks()
        {
            var books = _context.Books.ToList();
            return books;
        }

        public Book Create(Book book)
        {
            if (BookExists(book.Id))
                throw new ArgumentException("Book ID already exists. Please update the existing record or add the book with a new ID.");
            _context.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book Update(long id, Book book)
        {
            var existingBookFound = FindBookById(id);
            if (existingBookFound == null)
                throw new ArgumentException("The given book id does not exist. Please make sure you have the correct id and try again.");

            existingBookFound.Title = book.Title;
            existingBookFound.Description = book.Description;
            existingBookFound.Author = book.Author;
            existingBookFound.Genre = book.Genre;

            _context.SaveChanges();
            return book;
        }

        public Book FindBookById(long id)
        {
            var book = _context.Books.Find(id);
            return book;
        }

        public bool BookExists(long id)
        {
            return _context.Books.Any(b => b.Id == id);
        }

        public bool DeleteBookById(long id)
        {
            var book = FindBookById(id);

            if (book == null)
                throw new ArgumentException("The given book id does not exist. Please make sure you have the correct id and try again.");
            _context.Books.Remove(book);

            _context.SaveChanges();
            return true;
        }
    }
}

