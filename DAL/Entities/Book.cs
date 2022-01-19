using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsArchived { get; set; }
        public bool IsReserved { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
    }
}
