using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels.Users
{
    public class DeactivateUserViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        public List<string> UserCategories { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
    }
}
