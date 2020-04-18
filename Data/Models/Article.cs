using SportBox7.Data.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class Article
    {
        public Article()
        {
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }

        public int? TempArticleId { get; set; }

        public string CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModDate { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string H1Tag { get; set; }

        public string ImageUrl { get; set; }

        public bool EnableComments { get; set; }

        public string SourceURL { get; set; }

        public string SourceName { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ArticleState State { get; set; }

        public virtual ArticleSeoData ArticleSeoData { get; set; }

        public virtual ICollection<Comment> Comments { get; }

        public virtual User User { get; set; }

        public bool IsDeleted { get; set; }



    }
}
