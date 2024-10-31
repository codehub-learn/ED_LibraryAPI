namespace ED_LibraryAPI.Domain
{
    public class Author
    {
        public int Id { get; set; }
        required public string FirstName { get; set; }
        required public string LastName { get; set; }
        required public List<Book> Books { get; set; }
    }
}
