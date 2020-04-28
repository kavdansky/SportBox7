using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Areas.Editors.ViewModels.Content;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IEditorService
    {
        public bool AddNewDraft(AddArticleViewModel model);

        public List<SelectListItem> GetUserCategories(IHttpContextAccessor httpContextAccessor);

        public List<Category> GetAllCategories();

        public ICollection<AllArticlesViewModel> LoadAllDrafts(string userId);

        public EditArticleViewModel EditDraftGetModel(int draftId);

        bool EditDraft(EditArticleViewModel model);

        bool DeleteDraft(int draftId);

        public AllArticlesViewModel GetDeleteDraftModel(int draftId);

        public ICollection<AllArticlesViewModel> LoadArticlesForReview();

        public ICollection<AllArticlesViewModel> LoadAllPublishedArticles();
        

    }
}
