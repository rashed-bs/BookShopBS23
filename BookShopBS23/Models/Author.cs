namespace BookShopBS23.Models
{
    public class Author
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }

        public Byte[] AuthorPhoto { get; set; }

        // Navigation properties 
        public ICollection<Book> Books { get; set; }
    }
}
