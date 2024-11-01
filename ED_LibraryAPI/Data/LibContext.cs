using Microsoft.EntityFrameworkCore;
using ED_LibraryAPI.Domain;

namespace ED_LibraryAPI.Data
{
    public class LibContext: DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member>  Members { get; set; }

        public LibContext(DbContextOptions<LibContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                    new Author()
                    {
                        Id = -1,
                        FirstName = "Umberto",
                        LastName = "Eco",
                        Books = new List<Book>() 
                    },
                    new Author()
                    {
                        Id = -2,
                        FirstName = "Haruki",
                        LastName = "Murakami",
                        Books = new List<Book>()
                    }
                );
            
            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    Id = -1,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "jsmith@example.com",
                    RentedBooks = new List<Book>()
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    Id = -1,
                    Name = "The Name of the Rose",
                    Publisher = "Fixed House",
                    AuthorId = -1,
                    RentedToId = -1
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
