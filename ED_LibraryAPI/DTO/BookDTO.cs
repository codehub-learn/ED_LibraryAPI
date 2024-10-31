namespace ED_LibraryAPI.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public AuthorDTO? Author { get; set; }

    }
}
