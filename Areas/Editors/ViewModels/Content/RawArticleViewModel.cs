using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels.Content
{
    public class RawArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string H1Tag { get; set; }

        public string ImageUrl { get; set; }

        public string SourceURL { get; set; }

        public string SourceName { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }
    }
}
