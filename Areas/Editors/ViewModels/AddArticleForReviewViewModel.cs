﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels
{
    public class AddArticleForReviewViewModel
    {

        public int TempArticleId { get; set; }

        public string CreatorId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string H1Tag { get; set; }

        public string ImageUrl { get; set; }

        public bool EnableComments { get; set; }

        public string SourceURL { get; set; }

        public string SourceName { get; set; }

        public SelectListItem Category { get; set; }

        [Required]
        public string MetaTitle { get; set; }

        [Required]
        public string MetaDescription { get; set; }

        [Required]
        public string MetaKeyword { get; set; }

        [Required]
        public string SeoUrl { get; set; }

    }
}
