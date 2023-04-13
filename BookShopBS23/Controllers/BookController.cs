using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShopBS23.Data;
using BookShopBS23.Models;

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
            var booksWithAuthors = bookShopDbContext.Books.Include(b => b.Author);
            return View(await booksWithAuthors.ToListAsync());
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
            return View(book);
        }

        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            var authors = await bookShopDbContext.Authors.ToListAsync();
            ViewBag.Authors = authors;
            return View();
        }

        // POST: Book/Create
        [HttpPost] // only post methods
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Description,publicationDate,ISBN,Genre,Language,AuthorId")] Book book, IFormFile CoverPhoto)
        {
            if (ModelState.IsValid)
            {
                if(CoverPhoto != null && CoverPhoto.Length > 0)
                {
                    byte[] bytes;
                    using (var ms = new MemoryStream()) // creating a memory stream
                    {
                        CoverPhoto.CopyTo(ms);
                        bytes = ms.ToArray();
                    }
                    book.CoverPhoto = bytes;
                }
                bookShopDbContext.Add(book); // adds the book 
                await bookShopDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // if Model state not valid 
            var authors = await bookShopDbContext.Authors.ToListAsync();
            ViewBag.Authors = authors;
            return View(book); // resending the book so the used might not reenter all the fields
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
            return View(book);
        }

        // POST: Books/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BookId,Title,Description,publicationDate,ISBN,Genre,Language,AuthorId")] Book book, IFormFile CoverPhoto)
        {
            if(id != book.BookId) // ensuring security 
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    byte[] bytes;
                    using (var ms = new MemoryStream()) // creating a memory stream
                    {
                        CoverPhoto.CopyTo(ms);
                        bytes = ms.ToArray();
                    }
                    book.CoverPhoto = bytes;
                    bookShopDbContext.Update(book); // updating to the database context

                    await bookShopDbContext.SaveChangesAsync();// saving the changes to the database
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!BookExists(book.BookId))
                    {
                        if(!BookExists(book.BookId))
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
            return View(book);
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
