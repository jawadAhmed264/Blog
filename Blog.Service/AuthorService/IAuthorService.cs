using System.Collections.Generic;
using ViewModel.AuthorViewModels;

namespace Blog.Service.AuthorService
{
    public interface IAuthorService
    {
        AuthorViewModel GetAuthorById(int? Id);
        AuthorViewModel GetAuthorByIdentityId(string Id);
        IList<AuthorViewModel> GetAllAuthors();
        int AddAuthor(AuthorViewModel model);
        int EditAuthor(AuthorViewModel model, int Id);
        int DeleteAuthor(int Id);
    }
}
