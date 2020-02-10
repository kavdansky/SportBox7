using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string CreatorId { get; set; }


        public virtual User User { get; set; }
    }
}
