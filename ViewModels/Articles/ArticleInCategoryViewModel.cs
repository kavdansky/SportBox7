using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.ViewModels.Articles
{
    public class ArticleInCategoryViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Creator { get; set; }

        public string CategoryEN { get; set; }

        public DateTime CreationDate { get; set; }

        public string Body { get; set; }

        public string H1Tag { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }

        public string SeoUrl { get; set; }
    }
}
