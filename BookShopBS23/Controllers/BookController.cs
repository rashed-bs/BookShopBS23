using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShopBS23.Data;
using BookShopBS23.Models;
using BookShopBS23.ViewModels;

namespace BookShopBS23.Controllers
{
    public class BookController : Controller
    {
        private readonly BookShopDbContex bookShopDbContext;

        public BookController(BookShopDbContex contex)
        {
            bookShopDbContext = contex;
        }


        // GET: Books
        public async Task<IActionResult> Index()
        {

            var booksWithAuthors = await bookShopDbContext.Books.Include(b => b.Author).ToListAsync();
            if(booksWithAuthors == null)
            {
                return NotFound();
            }

            var booksWithAuthorsViewModel = new List<BookIndexPageViewModel>();
            foreach( var book in booksWithAuthors )
            {
                var bookWithAuthorViewModel = new BookIndexPageViewModel()
                {
                    BookId = book.BookId,
                    Author = book.Author,
                    Title = book.Title,
                    PictureFormat = book.PictureFormat,
                    Description = book.Description,
                    Genre = book.Genre,
                    ISBN = book.ISBN,
                    publicationDate = book.publicationDate,
                    Language = book.Language,
                    CoverPhoto = Convert.ToBase64String(book.CoverPhoto),
                    AuthorId = book.AuthorId
                };
                booksWithAuthorsViewModel.Add(bookWithAuthorViewModel);
            }

            return View(booksWithAuthorsViewModel);
        }

        // GET: Book/Details/id
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || bookShopDbContext.Books == null)
            {
                return NotFound();
            }

            var book = await bookShopDbContext.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if(book == null)
            {
                return NotFound();
            }

            var bookDetailsViewModel = new BookDetailsPageViewModel()
            {
                BookId = book.BookId,
                Author = book.Author,
                ISBN = book.ISBN,
                PictureFormat = book.PictureFormat,
                AuthorId = book.AuthorId,
                Genre = book.Genre,
                Title = book.Title,
                publicationDate = book.publicationDate,
                Language = book.Language,
                Description = book.Description,
                CoverPhoto = Convert.ToBase64String(book.CoverPhoto)
            };



            return View(bookDetailsViewModel);
        }

        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            var authors = await bookShopDbContext.Authors.ToListAsync();
            ViewBag.Authors = authors;
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreationViewModel bookCreationViewModel)
        {
            if(bookCreationViewModel == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // convert viewModel object to book model object
                var book = new Book()
                {
                    Title = bookCreationViewModel.Title,
                    Genre = bookCreationViewModel.Genre,
                    Description = bookCreationViewModel.Description,
                    ISBN = bookCreationViewModel.ISBN,
                    Language = bookCreationViewModel.Language,
                    publicationDate = bookCreationViewModel.publicationDate,
                    AuthorId = bookCreationViewModel.AuthorId, 
                    PictureFormat = bookCreationViewModel.CoverPhoto.ContentType
                };
                // converting the coverPhoto from FormFile to byte array
                var memoryStream = new MemoryStream();
                bookCreationViewModel.CoverPhoto.CopyTo(memoryStream);
                book.CoverPhoto = memoryStream.ToArray();

                // getting the author of the book 
                var authorOfTheBook = await bookShopDbContext.Authors.FindAsync(bookCreationViewModel.AuthorId);
                if(authorOfTheBook == null)
                {
                    return Problem("Author of the book not found in the 'BookShopDbContext.Author' entity");
                }
                book.Author = authorOfTheBook;

                // adding to the context
                bookShopDbContext.Add(book); 
                await bookShopDbContext.SaveChangesAsync(); // adding to the database
                return RedirectToAction(nameof(Index));
            }
            // if Model state not valid 
            var authors = await bookShopDbContext.Authors.ToListAsync();
            ViewBag.Authors = authors;
            return View(bookCreationViewModel); // resending the bookCreationViewModel so the user might not reenter all the fields
        }

        // GET: Book/Edit/id
        public async Task<IActionResult> Edit(string id)
        {
            if(id ==  null || bookShopDbContext.Books == null)
            {
                return NotFound();
            }

            var book = await bookShopDbContext.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            var authors = bookShopDbContext.Authors.ToList();
            ViewBag.Authors = authors;
            // from Book model object to BookEditViewModel object
            var bookEditViewModel = new BookEditViewModel()
            {
                AuthorId = book.AuthorId,
                BookId = book.BookId,
                Genre = book.Genre,
                ISBN = book.ISBN,
                Language = book.Language,
                Title = book.Title,
                Description = book.Description,
                publicationDate = book.publicationDate
            };

            var stream = new MemoryStream(book.CoverPhoto);
            IFormFile formFile = new FormFile(stream, 0, book.CoverPhoto.Length, "name", "fileName");
            bookEditViewModel.CoverPhoto = formFile;

            return View(bookEditViewModel);
        }

        // POST: Books/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BookEditViewModel bookEditViewModel)
        {
            if(id != bookEditViewModel.BookId) // ensuring security 
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    var book = new Book()
                    {
                        Title = bookEditViewModel.Title,
                        Genre = bookEditViewModel.Genre,
                        Description = bookEditViewModel.Description,
                        ISBN = bookEditViewModel.ISBN,
                        Language = bookEditViewModel.Language,
                        publicationDate = bookEditViewModel.publicationDate,
                        AuthorId = bookEditViewModel.AuthorId,
                        PictureFormat = bookEditViewModel.CoverPhoto.ContentType
                    };
                    // converting the coverPhoto from FormFile to byte array
                    var memoryStream = new MemoryStream();
                    bookEditViewModel.CoverPhoto.CopyTo(memoryStream);
                    book.CoverPhoto = memoryStream.ToArray();

                    // getting the author of the book 
                    var authorOfTheBook = await bookShopDbContext.Authors.FindAsync(bookEditViewModel.AuthorId);
                    if (authorOfTheBook == null)
                    {
                        return Problem("Author of the book not found in the 'BookShopDbContext.Author' entity");
                    }
                    book.Author = authorOfTheBook;

                    // adding to the context
                    bookShopDbContext.Add(book);
                    await bookShopDbContext.SaveChangesAsync(); // adding to the database
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!BookExists(bookEditViewModel.BookId))
                    {
                        if(!BookExists(bookEditViewModel.BookId))
                        {
                            return NotFound();
                        } 
                        else
                        {
                            throw new Exception("Error from the BookController Post Edit method!");
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            var authors = await bookShopDbContext.Authors.ToListAsync();
            ViewBag.Authors = authors;
            return View(bookEditViewModel);
        }

        // GET: Book/Delete/id
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null || bookShopDbContext.Books == null)
            {
                return NotFound();
            }

            var book = await bookShopDbContext.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if(bookShopDbContext.Books == null)
            {
                return Problem("Entity set 'BookShopDbContex.Books'  is null.");
            }

            var book = await bookShopDbContext.Books.FindAsync(id);
            if(book != null)
            {
                bookShopDbContext.Books.Remove(book);
            }

            await bookShopDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return (bookShopDbContext.Books?.Any(b => b.BookId == id)).GetValueOrDefault();
        }
    }
}
