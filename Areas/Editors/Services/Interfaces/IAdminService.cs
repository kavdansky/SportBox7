using SportBox7.Areas.Editors.ViewModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IAdminService
    {
        void PublishArticle(int id);
        EditArticleViewModel EditArticleGetModel(int draftId);
        void EditArticle(EditArticleViewModel model);
        void UnPublish(int articleId);
        void DeleteArticle(int articleId);
    }
}
