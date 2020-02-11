using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Article> Articles { get; }

        
    }
}
