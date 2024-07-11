using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using BookStore.Service.Interface;
using BookStore.Service.Implementation;
using BookStore.Domain.Domain.Models;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text;
using ExcelDataReader;

namespace BookStore.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;
        private readonly IGenreService _genreService;

        public BooksController(IBookService booksService, IAuthorService authorService, IPublisherService publisherService, IGenreService genreService)
        {
            _bookService = booksService;    
            _authorService = authorService;
            _publisherService = publisherService;
            _genreService = genreService;
           
        }

        // GET: Books
        public IActionResult Index()
        {
            var books = _bookService.GetAll();
                //.OrderBy(b => b.Author?.FirstName).ToList();
            return View(books);
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetDetails(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            // Fetch the authors from the database
            var authors = _authorService.GetAll().Select(a => new {
                Id = a.Id,
                FullName = a.FirstName + " " + a.LastName  // Combine first and last names
            }).ToList();

            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName");
            ViewData["GenreId"] = new SelectList(_genreService.GetAll(), "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_publisherService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,BookImage,Title,Description,Price,PublisherId,AuthorId,GenreId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.CreateNewBook(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_authorService.GetAll(), "Id", "Id", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_genreService.GetAll(), "Id", "Name", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_publisherService.GetAll(), "Id", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetDetails(id);
            if (book == null)
            {
                return NotFound();
            }
            var authors = _authorService.GetAll().Select(a => new {
                Id = a.Id,
                FullName = a.FirstName + " " + a.LastName  // Combine first and last names
            }).ToList();
            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName");
            ViewData["GenreId"] = new SelectList(_genreService.GetAll(), "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_publisherService.GetAll(), "Id", "Name");


            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,BookImage,Title,Description,Price,PublisherId,AuthorId,GenreId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.UpdateExistingBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_authorService.GetAll(), "Id", "Id", book.AuthorId);

            ViewData["GenreId"] = new SelectList(_genreService.GetAll(), "Id", "Name", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_publisherService.GetAll(), "Id", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetDetails(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _bookService.GetDetails(id) != null;
        }

    
    }
}
