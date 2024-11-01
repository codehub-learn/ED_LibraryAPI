using ED_LibraryAPI.Data;
using ED_LibraryAPI.Domain;
using ED_LibraryAPI.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ED_LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly LibContext _context;
        public BookService(LibContext context)
        {
            _context = context;
        }

        public async Task<BookDTO> AddBook(BookDTO bookDTO)
        {
            Author? bookAuthor = await _context.Authors.Where(a => a.Id == bookDTO.Author.Id).SingleOrDefaultAsync();
            if (bookAuthor == null) return null;

            Book book = new Book()
            {
                Name = bookDTO.Name,
                Publisher = bookDTO.Publisher,
                Author = bookAuthor
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.ConvertBook();
        }

        public async Task<bool> Delete(int bookId)
        {
            Book? book = _context.Books.Where(b => b.Id == bookId).SingleOrDefault();
            if (book is null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
            return await _context.Books.Select(b => b.ConvertBook()).ToListAsync();
        }

        public async Task<BookDTO>? GetBook(int id)
        {
             Book? book = await _context.Books
                .Include(b => b.Author)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            if (book == null) return null;
            return book.ConvertBook(); // Converters.ConvertBook(book);

            //return new BookDTO()
            //{
            //    Id = book.Id,
            //    Name = book.Name,
            //    Publisher = book.Publisher,
            //    Author = new AuthorDTO()
            //    {
            //        Id = book.Author.Id,
            //        FirstName = book.Author.FirstName,
            //        LastName = book.Author.LastName,
            //    }
            //};
        }

        public async Task<BookDTO> Replace(int bookId, BookDTO bookDTO)
        {
            if (bookDTO.Author == null) return null;

            Book? book = await _context.Books
                .Where(b => b.Id == bookId).SingleOrDefaultAsync();
            if (book is null) return null;

            Author? author = await _context.Authors
                .Where(a => a.Id == bookDTO.Author.Id).SingleOrDefaultAsync();
            if (author is null) return null;

            book.Name = bookDTO.Name;
            book.Author = author;
            book.Publisher = bookDTO.Publisher;

            await _context.SaveChangesAsync();
            return book.ConvertBook();

        }

        public async Task<List<BookDTO>> Search
            (string? name, string? publisher, string? authorFirst, string? authorLast)
        {
            var results = _context.Books.Include(b => b.Author); //DONT add ToList, so it is not executed

            if (name is not null)
                results.Where(b => b.Name.ToLower().Contains(name.ToLower()));

            if (publisher is not null)
                results.Where (b => b.Publisher.ToLower().Contains(publisher.ToLower()));

            if (authorFirst is not null)
                results.Where(b => b.Author.FirstName.ToLower().Contains(authorFirst.ToLower()));
            
            if (authorLast is not null)
                results.Where(b => b.Author.LastName.ToLower().Contains(authorLast.ToLower()));

            var resultsList = await results.ToListAsync(); //Execution of query
            if (resultsList is null) return null;

            return resultsList.Select(b => b.ConvertBook()).ToList();
       
        }

        public async Task<BookDTO> UpdateBook(int bookId, BookDTO dto)
        {
            Book? book = await _context.Books.Include(b => b.Author).Where(b => b.Id == bookId)
                .SingleOrDefaultAsync();

            if (book is null) return null;

            if (dto.Name != null) book.Name = dto.Name;
            if (dto.Publisher != null) book.Publisher = dto.Publisher; 
            if (dto.Author != null)
            {
                Author? author = await _context.Authors.Where( a => a.Id == dto.Author.Id).SingleOrDefaultAsync();
                if (author == null) return null;
                if (author != null) book.Author = author;
            }

            await _context.SaveChangesAsync();
            return book.ConvertBook();
        }
    }
}
