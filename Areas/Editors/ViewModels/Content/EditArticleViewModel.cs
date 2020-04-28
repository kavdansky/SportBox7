using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels.Content
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Article Text")]
        [Required]
        public string Body { get; set; }

        [Display(Name = "Heading")]
        [Required]
        public string H1Tag { get; set; }

        [Display(Name = "Enable Comments")]
        public bool EnableComments { get; set; }

        [Display(Name = "Image name- fill only if the source is external")]
        public string ImageName { get; set; }

        public IFormFile ArticleImage { get; set; }

        [Display(Name = "Source URL")]
        [Url]
        public string SourceURL { get; set; }

        [Display(Name = "Source Name")]
        public string SourceName { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Image URL apply only for external files")]
        public string ImageUrl { get; set; }

        [Display(Name = "Meta Title(for seo purposes)")]
        [Required]
        public string MetaTitle { get; set; }

        [Display(Name = "Meta Description(for seo purposes)")]
        [Required]
        public string MetaDescription { get; set; }

        [Display(Name = "Meta Keywords  use , as separator(for seo purposes)")]
        [Required]
        public string MetaKeyword { get; set; }

        [Display(Name = "Name For Url(for seo purposes)")]
        public string SeoUrl { get; set; }
    }
}
