using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels.Users
{
    public class DeactivateUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
}
