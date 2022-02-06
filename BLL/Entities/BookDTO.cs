using DAL.Entities;
using System;

namespace BLL.Entities
{
    public class BookDTO : IEquatable<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsArchived { get; set; }
        public bool IsReserved { get; set; }
        public int? ClientId { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Book);
        }

        public bool Equals(Book other)
        {
            return other != null &&
                   Id == other.Id &&
                   Title == other.Title &&
                   Author == other.Author &&
                   IsArchived == other.IsArchived &&
                   IsReserved == other.IsReserved &&
                   ClientId == other.ClientId;
        }
    }
}
