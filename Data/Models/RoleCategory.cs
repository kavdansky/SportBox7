using SportBox7.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class RoleCategory
    {
        public string RoleId { get; set; }

        public Role Role { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
