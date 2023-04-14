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
    public class AuthorController : Controller
    {
        private readonly BookShopDbContex bookShopDbContext;

        public AuthorController(BookShopDbContex context)
        {
            bookShopDbContext = context;
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
            return bookShopDbContext.Authors != null ?
                        View(await bookShopDbContext.Authors.ToListAsync()) :
                        Problem("Entity set 'BookShopDbContex.Authors'  is null.");
        }

        // GET: Author/Details/id
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || bookShopDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = await bookShopDbContext.Authors
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            var authorDetailsViewModel = new AuthorDetailsViewModel()
            {
                AuthorEmail = author.AuthorEmail, 
                AuthorName = author.AuthorName,
                AuthorId = author.AuthorId,
                Description = author.Description,
                PictureFormat = author.
            }
            return View(author);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreationViewModel authorCreationViewModel)
        {

            if (authorCreationViewModel is null)
            {
                throw new ArgumentNullException(nameof(authorCreationViewModel));
            }
            if (ModelState.IsValid)
            {
                var author = new Author()
                {
                    AuthorName = authorCreationViewModel.AuthorName,
                    AuthorEmail = authorCreationViewModel.AuthorEmail,
                    Description = authorCreationViewModel.Description, 
                    PictureFormat = authorCreationViewModel.AuthorPhoto.ContentType
                };

                var memoryStream = new MemoryStream();
                authorCreationViewModel.AuthorPhoto.CopyTo(memoryStream);
                author.AuthorPhoto = memoryStream.ToArray();

                bookShopDbContext.Add(author);
                await bookShopDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(authorCreationViewModel);
        }

        // GET: Author/Edit/id
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || bookShopDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = await bookShopDbContext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var authorEditViewModel = new AuthorEditViewModel()
            {
                AuthorId = author.AuthorId,
                AuthorEmail = author.AuthorEmail,
                AuthorName = author.AuthorName,
                Description = author.Description,
            };

            // from byte array to formFile
            var stream = new MemoryStream(author.AuthorPhoto);
            IFormFile file = new FormFile(stream, 0, author.AuthorPhoto.Length, "name", "filename");
            authorEditViewModel.AuthorPhoto = file;

            return View(authorEditViewModel);
        }

        // POST: Author/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AuthorEditViewModel authorEditViewModel)
        {


            if (authorEditViewModel == null || id != authorEditViewModel.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var author = await bookShopDbContext.Authors.FindAsync(id);

                    if (author == null)
                    {
                        return NotFound();
                    }

                    // updating the author object 
                    author.AuthorName = authorEditViewModel.AuthorName;
                    author.AuthorEmail = authorEditViewModel.AuthorEmail;
                    author.Description = authorEditViewModel.Description;
                    author.PictureFormat = authorEditViewModel.AuthorPhoto.ContentType;

                    // from iformfile to byte array
                    var memoryStream = new MemoryStream();
                    authorEditViewModel.AuthorPhoto.CopyTo(memoryStream);
                    author.AuthorPhoto = memoryStream.ToArray();


                    bookShopDbContext.Update(author);
                    await bookShopDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(authorEditViewModel.AuthorId))
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
            return View(authorEditViewModel);
        }

        // GET: Author/Delete/id
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || bookShopDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = await bookShopDbContext.Authors
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Author/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (bookShopDbContext.Authors == null)
            {
                return Problem("Entity set 'BookShopDbContex.Authors'  is null.");
            }
            var author = await bookShopDbContext.Authors.FindAsync(id);
            if (author != null)
            {
                bookShopDbContext.Authors.Remove(author);
            }

            await bookShopDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(string id)
        {
            return (bookShopDbContext.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}
