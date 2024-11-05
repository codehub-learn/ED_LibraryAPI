using ED_LibraryAPI.Data;
using ED_LibraryAPI.DTO;
using ED_LibraryAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace ED_LibraryAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private LibContext _libContext;
        public AuthorService(LibContext context) 
        {
            _libContext = context;
        }

        public async Task<AuthorDTO> AddAuthor(AuthorDTO dto)
        {
            if (dto.FirstName == null || dto.LastName == null)
                throw new BadRequestException
                    ("Bad Request: The author first and last name must be specified!");

            Author a = new Author()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Books = new List<Book>()
            };

            await _libContext.Authors.AddAsync(a);
            _libContext.SaveChanges();

            return a.ConvertAuthor();
        } 

        public async Task<bool> DeleteAuthor(int id)
        {
           Author? a = await _libContext.Authors.FindAsync(id);

           if (a == null) { return false; }
           else
            {
                _libContext.Authors.Remove(a);
                await _libContext.SaveChangesAsync();
                return true;
            };
        }

        public async Task<List<AuthorDTO>> GetAllAuthors()
        {
            return await _libContext.Authors.Select(a => a.ConvertAuthor()).ToListAsync();
        }

        public async Task<List<AuthorWithBooksDTO>> GetAllAuthorsWithBooks()
        {
            return await _libContext.Authors.Select(a => a.ConvertAuthorWithBooks()).ToListAsync();
        }

        public async Task<AuthorDTO?> GetAuthorById(int id)
        {
            return await _libContext.Authors
                .Select(a => a.ConvertAuthor())
                .SingleOrDefaultAsync(a => a.Id == id);
     
        }

        public async Task<AuthorWithBooksDTO?> GetAuthorWithBooksById(int id)
        {
            return await _libContext.Authors
                .Select(a => a.ConvertAuthorWithBooks())
                .SingleOrDefaultAsync (a => a.Id == id);
        }

        public async Task<AuthorDTO> ReplaceAuthor(AuthorDTO dto)
        {
            if (dto.FirstName == null || dto.LastName == null)
                throw new BadRequestException("Bad Request: The author first and last name must be specified!");

            var author = await _libContext.Authors.FindAsync(dto.Id);

            if (author == null)
                throw new NotFoundException("Not Found: The author with the given id was not found!");

            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            await _libContext.SaveChangesAsync();

            return author.ConvertAuthor();
        }

        public async Task<List<AuthorDTO>> SearchAuthor(string firstName, string lastName)
        {
            IQueryable<Author> search = _libContext.Authors;
            
            if (firstName is not null) search = search.Where(a => a.FirstName == firstName);
            if (lastName is not null) search = search.Where(l => l.LastName == lastName);

            return await search
                .Select(a => a.ConvertAuthor())
                .ToListAsync();
        }

        public async Task<AuthorDTO> UpdateAuthor(AuthorDTO dto)
        {
            bool firstNameNull = (dto.FirstName == null) ? true : false;
            bool lastNameNull = (dto.LastName == null) ? true : false;

            if (!firstNameNull && !lastNameNull) throw new BadRequestException
                    ("Bad Request: Either the author first name or last name must be specified");

            var author = await _libContext.Authors.FindAsync(dto.Id);

            if (author == null)
                throw new NotFoundException("Not Found: The author with the given id was not found!");

            if (!firstNameNull)
                author.FirstName = dto.FirstName!;
            if (!lastNameNull)
                author.LastName = dto.LastName!;

            return author.ConvertAuthor();
        }
    }
}
