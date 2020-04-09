using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels.Content
{
    public class AllArticlesViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Date created")]
        public DateTime CreationDate { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }
        
    }
}
