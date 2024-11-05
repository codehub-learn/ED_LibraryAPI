using ED_LibraryAPI.DTO;
namespace ED_LibraryAPI.Services
{
    public interface IAuthorService
    {
        public Task<List<AuthorDTO>> GetAllAuthors();
        public Task<List<AuthorWithBooksDTO>> GetAllAuthorsWithBooks();
        public Task<AuthorDTO?> GetAuthorById (int id);
        public Task<AuthorWithBooksDTO?> GetAuthorWithBooksById (int id);
        public Task<List<AuthorDTO>> SearchAuthor(string firstName, string lastName);
        public Task<AuthorDTO> AddAuthor(AuthorDTO dto);
        public Task<AuthorDTO> UpdateAuthor(AuthorDTO dto);
        public Task<AuthorDTO> ReplaceAuthor (AuthorDTO dto);
        public Task<bool> DeleteAuthor (int id);
    }
}
