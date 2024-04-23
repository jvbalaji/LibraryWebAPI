using LibraryWebAPI.Models;

namespace LibraryWebAPI.Services
{
    public class InMemoryBookRepository : IBookRepository
    {
        private List<Book> _books;

        public InMemoryBookRepository()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "Dune", Author = "Frank Herbert", Year = 1965, Status = "Available", BorrowerName = "" },
                new Book { Id = 2, Title = "Neuromancer", Author = "William Gibson", Year = 1984, Status = "Available", BorrowerName = "" },
                new Book { Id = 3, Title = "Foundation", Author = "Isaac Asimov", Year = 1951, Status = "Available", BorrowerName = "" },
                new Book { Id = 4, Title = "Snow Crash", Author = "Neal Stephenson", Year = 1992, Status = "Available", BorrowerName = "" },
                new Book { Id = 5, Title = "The Left Hand of Darkness", Author = "Ursula K. Le Guin", Year = 1969, Status = "Available", BorrowerName = "" }
            };
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            if (_books.Any())
            {
                book.Id = _books.Max(b => b.Id) + 1;
            }
            else
            {
                // If _books collection is empty, assign the ID of 1
                book.Id = 1;
            }

            _books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Year = book.Year;
            }
        }

        public void DeleteBook(int id)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);
            }
        }
    }

}
