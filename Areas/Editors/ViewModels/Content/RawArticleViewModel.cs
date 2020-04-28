using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels.Content
{
    public class RawArticleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Article text")]
        public string Body { get; set; }

        [Display(Name = "Heading")]
        public string H1Tag { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Source Url")]
        public string SourceURL { get; set; }

        [Display(Name = "Source name")]
        public string SourceName { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
    }
}
