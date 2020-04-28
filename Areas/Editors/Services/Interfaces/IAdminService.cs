using SportBox7.Areas.Editors.ViewModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IAdminService
    {
        public bool PublishArticle(int id);
        public EditArticleViewModel EditArticleGetModel(int draftId);
        public bool EditArticle(EditArticleViewModel model);
        public bool UnPublish(int articleId);
        public bool DeleteArticle(int articleId);
    }
}
