namespace ED_LibraryAPI.DTO
{
    public class BookRentalDTO : BookDTO
    {
        public MemberDTO? RentedTo { get; set; }
    }
}
