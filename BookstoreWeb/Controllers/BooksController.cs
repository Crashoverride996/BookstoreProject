using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using BookstoreAplication.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly IGetBookCommand _getBook;
        private readonly IGetBooksWebCommand _getBooks;
        private readonly IAddBookCommand _addBook;
        private readonly IEditBookCommand _editBook;
        private readonly IDeleteBookCommand _delBook;

        public BooksController(IGetBookCommand getBook, IGetBooksWebCommand getBooks, IAddBookCommand addBook, IEditBookCommand editBook, IDeleteBookCommand delBook)
        {
            _getBook = getBook;
            _getBooks = getBooks;
            _addBook = addBook;
            _editBook = editBook;
            _delBook = delBook;
        }

        // GET: Books
        public ActionResult Index(BookSearch search)
        {
            var books = _getBooks.Execute(search);
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            var book = _getBook.Execute(id);
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _addBook.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Book with that title already exists";
            }
            catch (Exception e)
            {
                TempData["error"] = e.ToString();
            }
            return View();
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateBookDto book)
        {
            book.Id = id;
            try
            {
                _editBook.Execute(book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Delete/5
        
        public ActionResult Delete(int id)
        {
            var book = _getBook.Execute(id);
            return View(book);
        }
        

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _delBook.Execute(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}