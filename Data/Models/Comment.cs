using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string CommenterName { get; set; }

        public string CommentBody { get; set; }

        public virtual Article Article { get; set; }
    }
}
