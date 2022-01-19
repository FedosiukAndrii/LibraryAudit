namespace BLL.Entities
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsArchived { get; set; }
        public bool IsReserved { get; set; }
        public int? ClientId { get; set; }
    }
}
