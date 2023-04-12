using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopBS23.Models
{
    public class Book
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime publicationDate { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }

        public Byte[] CoverPhoto { get; set; }

        // navigation properties 
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
