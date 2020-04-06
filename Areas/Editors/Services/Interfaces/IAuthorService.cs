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
        void AddNewDraft(AddArticleViewModel model);
        EditArticleViewModel EditDraftGetModel(int draftId);
        void EditDraft(EditArticleViewModel model);
        void SentDraftForReview(int articleId);
        ICollection<AllArticlesViewModel> LoadMyArticlesForReview(string userId);

    }


}
