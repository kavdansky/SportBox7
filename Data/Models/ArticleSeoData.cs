using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class ArticleSeoData
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public string SeoUrl { get; set; }

        public Article Article { get; set; }
    }
}
