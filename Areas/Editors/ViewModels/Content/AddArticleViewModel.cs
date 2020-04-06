using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels
{
    public class AddArticleViewModel
    {

        public int TempArticleId { get; set; }

        public string CreatorId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string H1Tag { get; set; }

        
        public string ImageName { get; set; }

        public bool EnableComments { get; set; }

        [Url]
        public string SourceURL { get; set; }

        public string SourceName { get; set; }

        public int CategoryId { get; set; }

        public IFormFile ArticleImage { get; set; }

        [Required]
        public string MetaTitle { get; set; }

        [Required]
        public string MetaDescription { get; set; }

        [Required]
        public string MetaKeyword { get; set; }

        [Required]
        [Url]
        public string SeoUrl { get; set; }


    }
}
