using SportBox7.Areas.Editors.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IAuthorService
    {
        void AddArticleForReview(AddArticleForReviewViewModel model);
    }
}
