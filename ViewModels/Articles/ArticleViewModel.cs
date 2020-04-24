using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.ViewModels.Articles
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            this.Likes = new int[2];
        }

        public int Id { get; set; }

        public string Creator { get; set; }

        public string CategoryEN { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModDate { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string H1Tag { get; set; }

        public string ImageUrl { get; set; }

        public bool EnableComments { get; set; }

        public string SourceURL { get; set; }

        public string SourceName { get; set; }

        public string Category { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public string SeoUrl { get; set; }

        public int[] Likes { get; set; }

        //public virtual ICollection<Comment> Comments { get; }

 
    }
}
