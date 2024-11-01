using ED_LibraryAPI.Data;
using ED_LibraryAPI.Domain;
namespace ED_LibraryAPI.DTO
{
    public static class Converters
    {
        public static BookDTO ConvertBook(this Book book)
        {
            return new BookDTO()
            {
                Id = book.Id,
                Name = book.Name,
                Publisher = book.Publisher,
                Author = new AuthorDTO()
                {
                    Id = book.Author.Id,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName,
                }
            };
        }
    }
}
