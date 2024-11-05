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

        public static AuthorDTO ConvertAuthor(this Author author)
        {
            return new AuthorDTO()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public static AuthorWithBooksDTO ConvertAuthorWithBooks (this Author author)
        {
            return new AuthorWithBooksDTO()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Books = author.Books.Select(b => b.ConvertBook()).ToList()
            };
        }
    }
}
