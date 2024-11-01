namespace ED_LibraryAPI.DTO
{
    public class AuthorWithBooksDTO : AuthorDTO
    {
        public List<BookDTO>? Books { get; set; }
    }
}
