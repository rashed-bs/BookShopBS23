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
using BookShopBS23.Service;
using BookShopBS23.IService;

namespace BookShopBS23.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BookShopDbContex bookShopDbContext;
        private readonly IAuthorService authorService;

        // Dependencies injection
        public AuthorController(BookShopDbContex context, IAuthorService author)
        {
            bookShopDbContext = context;
            authorService = author;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "List of all the authors";

            if (bookShopDbContext.Authors == null)
            {
                return NotFound();
            }
            var authors = authorService.GetAuthorsAsync();
            var authorsViewModel = authorService.AuthorToAuthorViewModelEnumerable(await authors);
            return View(authorsViewModel);
        }

        // GET: Author/Details/id
        public async Task<IActionResult> Details(string id)
        {
            ViewBag.Title = "Author";

            if (id == null || bookShopDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = authorService.FindByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorViewModel = authorService.AuthorToAuthorViewModel(await author);

            return View(authorViewModel);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            ViewBag.Title = "Create an Author";
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorViewModel authorViewModel)
        {

            if (authorViewModel is null)
            {
                throw new ArgumentNullException(nameof(authorViewModel));
            }
            if (ModelState.IsValid)
            {
                var author = authorService.AuthorViewModelToAuthor(authorViewModel);
                await authorService.SaveAsync(author);
                return RedirectToAction("Index");
            }
            return View(authorViewModel);
        }

        // GET: Author/Edit/id
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Title = "Updation of Author Details";
            
            if (id == null || bookShopDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = await authorService.FindByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var authorViewModel = authorService.AuthorToAuthorViewModel(author);

            return View(authorViewModel);
        }

        // POST: Author/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AuthorViewModel authorViewModel)
        {
            if (authorViewModel == null || id != authorViewModel.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var author = authorService.AuthorViewModelToAuthor(authorViewModel);
                    await authorService.UpdateAsync(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(authorViewModel.AuthorId))
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
            return View(authorViewModel);
        }

        // GET: Author/Delete/id
        public async Task<IActionResult> Delete(string id)
        {
            ViewBag.Title = "Deletion of Author";

            if (id == null || bookShopDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = await authorService.FindByIdAsync(id); // author with books
            if (author == null)
            {
                return NotFound();
            }

            var authorViewModel = authorService.AuthorToAuthorViewModel(author);

            return View(authorViewModel);
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
                await authorService.DeleteAsync(author);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(string id)
        {
            return (bookShopDbContext.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}
