using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string CategoryNameEN { get; set; }

        public string CategoryNameSportsDb { get; set; }

        public ICollection<Article> Articles { get; }

        public ICollection<UserCategory> UserCategories { get;  }
    }
}
