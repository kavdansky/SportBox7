using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class SocialSignal
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public bool IsLiked { get; set; }

        public string VisitorIp { get; set; }
    }
}
