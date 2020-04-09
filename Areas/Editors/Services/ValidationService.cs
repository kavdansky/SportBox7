using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Data;
using SportBox7.Data.Enums;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services
{
    public class ValidationService : IValidationService
    {
        private readonly ApplicationDbContext dbContext;

        public ValidationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CheckArticleUserPermissions(string userId)
        {
           
            string userRoleId = dbContext.UserRoles.Where(u => u.UserId == userId).FirstOrDefault().RoleId;
            string[] permittedRoleIds = dbContext.Roles.Where(r => r.NormalizedName == "ADMIN" || r.NormalizedName == "CHIEFEDITOR").Select(x=> x.Id).ToArray();
            for (int i = 0; i < permittedRoleIds.Length; i++)
            {
                if (userRoleId == permittedRoleIds[i])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckDraftUserPermissions(string userId, int draftId)
        {
            Article articleToCheck = dbContext.Articles.Find(draftId);
            if (articleToCheck?.CreatorId == userId && articleToCheck.State == ArticleState.Draft)
            {
                return true;
            }
            return false;
        }
    }
}
