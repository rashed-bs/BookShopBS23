﻿using Microsoft.Build.Framework;

namespace BookShopBS23.ViewModels
{
    public class AuthorCreationViewModel
    {
        [Required]
        public string AuthorName { get; set; }

        [Required]
        public string AuthorEmail { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile AuthorPhoto { get; set; }
    }
}
