﻿using BookShopBS23.Models;
using BookShopBS23.ViewModels;

namespace BookShopBS23.IService
{
    public interface IAuthorService
    {
        AuthorViewModel AuthorToAuthorViewModel(Author author);
        IEnumerable<AuthorViewModel> AuthorToAuthorViewModelEnumerable(IEnumerable<Author> author);
        Author AuthorViewModelToAuthor(AuthorViewModel authorViewModel);
        public Task SaveAsync(Author author);
        public Task UpdateAsync (Author author);
        public Task DeleteAsync(Author author);
        public Task<List<Author>> GetAuthorsAsync();
        public Task<List<Author>> GetALlAuthorWithBooksAsync();
        public  Task<Author?> FindByIdAsync(string id);
    }
}
