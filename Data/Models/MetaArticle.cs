using SportBox7.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class MetaArticle
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public string SourceName { get; set; }

        public string SourceUrl { get; set; }

        public ArticleCategory Catgory { get; set; }
    }
}
