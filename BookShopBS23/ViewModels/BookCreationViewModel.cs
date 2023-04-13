using BookShopBS23.Models;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopBS23.ViewModels
{
    public class BookCreationViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime publicationDate { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public IFormFile CoverPhoto { get; set; }

        [Required]
        public string AuthorId { get; set; }

    }
}
