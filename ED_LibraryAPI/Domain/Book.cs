namespace ED_LibraryAPI.Domain
{
    public class Book
    {
        public int Id { get; set; }
        required public string Name { get; set; }
        public Author? Author { get; set; }
        public int? AuthorId { get; set; }
        required public string Publisher { get; set; }
        public Member? RentedTo { get; set; }
        public int? RentedToId { get; set; }
    }
}
