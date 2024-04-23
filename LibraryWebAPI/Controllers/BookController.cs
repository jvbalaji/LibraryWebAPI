using Microsoft.AspNetCore.Mvc;
using LibraryWebAPI.Models;
using LibraryWebAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebAPI.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: /Book/ShowCatalog
        public IActionResult ShowCatalog()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }

        // GET: /Book/Borrow
        public IActionResult Borrow()
        {
            var availableBooks = _bookRepository.GetAllBooks().Where(b => b.Status == "Available").ToList();
            return View(availableBooks);
        }

        // POST: /Book/Borrow
        [HttpPost]
        public IActionResult Borrow(int bookId, string borrowerName)
        {
            var book = _bookRepository.GetBookById(bookId);

            if (book == null || book.Status != "Available")
            {
                return NotFound(); // Book not found or not available
            }

            // Update book status to "Borrowed" and record borrower information
            book.Status = "Borrowed";
            book.BorrowerName = borrowerName;

            _bookRepository.UpdateBook(book);

            return RedirectToAction("Index", "Home"); // Redirect to home page after borrowing
        }

        // GET: /Book/Return
        public IActionResult Return()
        {
            var borrowedBooks = _bookRepository.GetAllBooks().Where(b => b.Status == "Borrowed").ToList();

            if (!borrowedBooks.Any())
            {
                ViewBag.Message = "No books are currently borrowed.";
            }

            return View(borrowedBooks);
        }

        // POST: /Book/Return
        [HttpPost]
        public IActionResult Return(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);

            if (book == null || book.Status != "Borrowed")
            {
                return NotFound(); // Book not found or not borrowed
            }

            // Update book status to "Available" and clear borrower name
            book.Status = "Available";
            book.BorrowerName = "";

            _bookRepository.UpdateBook(book);

            return RedirectToAction("Index", "Home"); // Redirect to home page after returning book
        }

        // GET: /Book/AddBook
        public IActionResult AddBook()
        {
            return View();
        }

        // POST: /Book/AddBook
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Status = "Available";
                book.BorrowerName = "";

                _bookRepository.AddBook(book);

                TempData["SuccessMessage"] = "New book added successfully!";

                return RedirectToAction("AddBookSuccess");
            }

            return View(book);
        }

        // GET: /Book/AddBookSuccess
        public IActionResult AddBookSuccess()
        {
            string? successMessage = TempData["SuccessMessage"] as string;
            ViewBag.SuccessMessage = successMessage;

            return View();
        }

        // GET: /Book/Delete
        public IActionResult Delete()
        {
            var books = _bookRepository.GetAllBooks().Where(b => b.Status == "Available").ToList();

            if (books == null || !books.Any())
            {
                return View(new List<Book>()); // Return an empty list to avoid null model
            }

            return View(books);
        }

        // POST: /Book/Delete
        [HttpPost]
        public IActionResult Delete(int[] bookIds)
        {
            if (bookIds == null || bookIds.Length == 0)
            {
                return RedirectToAction("Delete");
            }

            foreach (var bookId in bookIds)
            {
                _bookRepository.DeleteBook(bookId);
            }

            return RedirectToAction("Delete"); // Redirect to refresh the view
        }
    }
}
