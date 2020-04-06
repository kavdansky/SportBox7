using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels.Users
{
    public class AllUsersViewModel
    {
        public AllUsersViewModel()
        {
            this.UserCategories = new List<string>();
        }
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public List<string> UserCategories { get; set; }

        public bool IsActive { get; set; }
    }
}
