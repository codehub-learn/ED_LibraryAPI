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
            return await _libContext.Authors
                .Select(a => a.ConvertAuthor())
                .ToListAsync();
        }

        public async Task<List<AuthorWithBooksDTO>> GetAllAuthorsWithBooks()
        {
            return await _libContext.Authors
                .Include(a => a.Books)
                .Select(a => a.ConvertAuthorWithBooks())
                .ToListAsync();
        }

        public async Task<AuthorDTO?> GetAuthorById(int id)
        {
            return await _libContext.Authors
                .Where(a => a.Id == id)
                .Select(a => a.ConvertAuthor())
                .SingleOrDefaultAsync();
        }

        public async Task<AuthorWithBooksDTO?> GetAuthorWithBooksById(int id)
        {
            return await _libContext.Authors
                //.Include(a => a.Books.Where(b => b.Name == "The Name of the Rose"))
                .Include(a => a.Books)
                .Where(a => a.Id == id)
                .Select(a => a.ConvertAuthorWithBooks()) //foreach author a in authors list.add(a.Convert())
                .SingleOrDefaultAsync();
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

        public async Task<List<AuthorDTO>> SearchAuthor(string? firstName, string? lastName)
        {
            IQueryable<Author> search = _libContext.Authors; //SELECT * FROM Authors
            
            if (firstName is not null) search = search.Where(a => a.FirstName == firstName); //If a firstname is given += WHERE FirstName = FirstName
            if (lastName is not null) search = search.Where(l => l.LastName == lastName); //If a lastname is given += WHERE LastName = LastName

            return await search
                .Select(a => a.ConvertAuthor())
                .ToListAsync();
        }

        public async Task<AuthorDTO> UpdateAuthor(AuthorDTO dto)
        {
            //Check whether first name and last name are nulls and save matching bools
            bool firstNameNull = (dto.FirstName == null) ? true : false;
            bool lastNameNull = (dto.LastName == null) ? true : false;

            //If both first name and last name are null, there is nothing update so we return an exception
            if (firstNameNull && lastNameNull) throw new BadRequestException
                    ("Bad Request: Either the author first name or last name must be specified");

           //If we have something to update, we search for the author with the matching id in the database
            var author = await _libContext.Authors.FindAsync(dto.Id);

            //If we cannot find an author with the given id in the database we can't update so we return an exception
            if (author == null)
                throw new NotFoundException("Not Found: The author with the given id was not found!");

            //If an updated exists for firstname we perform the update
            if (!firstNameNull)
                author.FirstName = dto.FirstName!;

            //If an updated exists for lastname we perform the update
            if (!lastNameNull)
                author.LastName = dto.LastName!;

            //Save changes to the database
            _libContext.SaveChanges();

            //Return the new item status
            return author.ConvertAuthor();
        }
    }
}
