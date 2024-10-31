namespace ED_LibraryAPI.DTO
{
    public class BookFlatDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public int AuthorId { get; set; }
        public string? AuhtorFistName { get; set; }
        public string? AutorLastName { get; set; }
    }
}
