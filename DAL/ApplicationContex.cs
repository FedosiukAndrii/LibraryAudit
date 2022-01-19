using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public class ApplicationContex : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public ApplicationContex(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-GR4PC1R;Database=LibraryAudit;Trusted_Connection=True;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book[]
                {
                new Book { Title = "Harry Potter and the Deathly Hallows", Author = "Rowling, J.K.", IsArchived = false , IsReserved = false, Id = 1},
                new  Book { Title = "Harry Potter and the Philosopher's Stone", Author = "Rowling, J.K.", IsArchived = false,IsReserved = false, Id = 2 },
                new   Book { Title = "Da Vinci Code,The", Author = "Brown, Dan", IsArchived = false,IsReserved = false, Id = 3 },
                new   Book { Title = "Fifty Shades Darker", Author = "James, E. L.", IsArchived = false,IsReserved = false, Id = 4 },
                new   Book { Title = "Twilight", Author = "Meyer, Stephenie", IsArchived = false,IsReserved = false, Id = 5 },
                new   Book { Title = "Girl with the Dragon Tattoo,The:Millennium Trilogy", Author = "Larsson, Stieg", IsArchived = false,IsReserved = false, Id = 6 },
                new   Book { Title = "Fifty Shades Freed", Author = "James, E. L", IsArchived = false,IsReserved = false, Id = 7 },
                new   Book { Title = "Lost Symbol,The", Author = "Brown, Dan", IsArchived = false, IsReserved = false, Id = 8 },
                new   Book { Title = "New Moon", Author = "Meyer, Stephenie", IsArchived = false, IsReserved = false, Id = 9 },
                });
        }
    }
}
