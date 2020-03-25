using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IEditorService
    {
        void AddArticleForReview(AddArticleForReviewViewModel model);

        public List<SelectListItem> GetUserCategories(IHttpContextAccessor httpContextAccessor);

        public List<Category> GetAllCategories();
    }
}
