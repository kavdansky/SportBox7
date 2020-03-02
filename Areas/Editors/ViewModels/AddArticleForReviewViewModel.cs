using SportBox7.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels
{
    public class AddArticleForReviewViewModel
    {

        public int TempArticleId { get; set; }

        public string CreatorId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string H1Tag { get; set; }

        public string ImageUrl { get; set; }

        public bool EnableComments { get; set; }

        public string SourceURL { get; set; }

        public string SourceName { get; set; }

        public ArticleCategory Category { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public string SeoUrl { get; set; }

    }
}
