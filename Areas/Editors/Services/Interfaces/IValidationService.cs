using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IValidationService
    {
        bool CheckDraftUserPermissions(string userId, int draftId);

        bool CheckArticleUserPermissions(string userId);

        
    }
}
