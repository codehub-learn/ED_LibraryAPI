using ED_LibraryAPI.DTO;
namespace ED_LibraryAPI.Services
{
    public interface IBookService
    {
        public Task<BookDTO>? GetBook(int id); //GET
        public Task<List<BookDTO>> GetAllBooks(); //GET
        public Task<BookDTO> AddBook(BookDTO bookDTO); //POsT
        public Task<List<BookDTO>> Search
            (string? name, string? publisher, string? authorFirst, string? authorLast); // GET
        public Task<BookDTO> UpdateBook(int bookId, BookDTO bookDTO); //PATCH
        public Task<BookDTO> Replace (int bookId, BookDTO bookDTO); //PUT
        public Task<bool> Delete(int bookId);
    }
}
