using BookShopBS23.Models;
using Microsoft.Build.Framework;


namespace BookShopBS23.ViewModels
{
    public class AuthorDetailsViewModel
    {
        [Required]
        public string AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public string AuthorEmail { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AuthorPhoto { get; set; }

        [Required]
        public string PictureFormat { get; set; }

        public ICollection<BookDetailsPageViewModel>? Books { get; set; }

    }
}
