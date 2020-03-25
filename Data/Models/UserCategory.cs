using SportBox7.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class UserCategory
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
