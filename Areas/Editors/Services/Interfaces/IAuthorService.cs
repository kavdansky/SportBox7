using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Areas.Editors.ViewModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IAuthorService
    {
        
        void SentDraftForReview(int articleId);
        ICollection<AllArticlesViewModel> LoadMyArticlesForReview(string userId);
        ICollection<AllArticlesViewModel> LoadMyPublishedArticles(string userId);

    }


}
