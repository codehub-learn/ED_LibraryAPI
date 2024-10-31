using System.ComponentModel.DataAnnotations;

namespace ED_LibraryAPI.Domain
{
    public class Member
    {
        public int Id { get; set; }
        required public string FirstName { get; set; }  //NOT NULL in Database
        required public string LastName { get; set; }   //NOT NULL in Database
        required public string? Email { get; set; }     //Allows NULLs in Database
        required public List<Book> RentedBooks { get; set; }
    }
}
