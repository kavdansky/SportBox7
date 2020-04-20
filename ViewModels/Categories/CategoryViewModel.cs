using SportBox7.ViewModels.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.ViewModels.Categories
{
    public class CategoryViewModel
    {

        public CategoryViewModel()
        {
            this.Articles = new List<ArticleViewModel>();
        }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryNameEN { get; set; }

        public IList<ArticleViewModel> Articles { get; set; }

    }
}
