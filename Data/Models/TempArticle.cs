using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class TempArticle
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string H1Tag { get; set; }

        public string ImageUrl { get; set; }

        public string SourceURL { get; set; }

        public string SourceName { get; set; }

        public DateTime Date { get; set; }
    }
}
