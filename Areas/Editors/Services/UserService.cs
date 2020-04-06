using Microsoft.AspNetCore.Identity;
using SportBox7.Areas.Editors.Services.Interfaces;
using SportBox7.Areas.Editors.ViewModels.Users;
using SportBox7.Data;
using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IEditorService editorService;
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly UserManager<User> userManager;

        public UserService(ApplicationDbContext dbContext,
            IEditorService editorService,
            RoleManager<IdentityRole> rolemanager,
            UserManager<User> userManager)
        {
            this.editorService = editorService;
            this.dbContext = dbContext;
            this.rolemanager = rolemanager;
            this.userManager = userManager;
        }

        public bool ActivateUser(DeactivateUserViewModel model)
        {
            User userToDeactivate = dbContext.Users.Find(model?.Id);
            if (userToDeactivate != null)
            {
                userToDeactivate.IsActive = true;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }


        public bool DeactivateUser(DeactivateUserViewModel model)
        {
            User userToDeactivate = dbContext.Users.Find(model?.Id);
            if (userToDeactivate != null)
            {
                userToDeactivate.IsActive = false;
                dbContext.SaveChanges();
                return true;
            }

            return false;
          
        }

        public async Task<bool> EditUserAsync(EditUserViewModel model)
        {
            User userToEdit = dbContext.Users.Where(u => u.Id == model.Id).FirstOrDefault();
            List<Category> allCategories = editorService.GetAllCategories();

            if (userToEdit != null)
            {
                userToEdit.Email = model?.Email;
                userToEdit.UserAdCode = model.UserAdCode;
                var userRole = await userManager.GetRolesAsync(userToEdit).ConfigureAwait(false);
                if (model.Role != userRole[0])
                {
                    await userManager.RemoveFromRoleAsync(userToEdit, userRole[0]).ConfigureAwait(false);
                    await userManager.AddToRoleAsync(userToEdit, model.Role).ConfigureAwait(false);
                }

                var userCategories = dbContext.UserCategories.Where(x => x.UserId == userToEdit.Id);
                foreach (var cat in userCategories)
                {
                    dbContext.UserCategories.Remove(cat);
                }

                foreach (var category in model.UserCategories)
                {
                    
                    if (category.Value)
                    {
                        UserCategory userCat = new UserCategory();
                        userCat.CategoryId = category.Key;
                        userCat.UserId = userToEdit.Id;
                        dbContext.UserCategories.Add(userCat);
                    }
                    
                }
                await dbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }

            return false;
        }

        public List<AllUsersViewModel> GetAllUsers()
        {
            var users = dbContext.Users.ToArray();
            return users.Select(x => new AllUsersViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Role = GetCurrentUserRole(x),
                UserCategories = GetCurrentUserCategoriesAsString(x),
                IsActive = x.IsActive
                
                
                
            }).ToList();
        }

        public DeactivateUserViewModel GetUser(string id)
        {
            DeactivateUserViewModel result = new DeactivateUserViewModel();
            var user = dbContext.Users.Find(id);
            result.Id = id;
            result.Role = GetCurrentUserRole(user);
            result.UserName = user.UserName;
            
            
            return result;
        }

        public EditUserViewModel GetUserToEdit(string userId)
        {
            var allCategories = editorService.GetAllCategories();
            User user = dbContext.Users.Find(userId);
            EditUserViewModel result = new EditUserViewModel();
            result.Id = user.Id;
            result.Email = user.Email;
            result.Role = GetCurrentUserRole(user);
            result.UserAdCode = user.UserAdCode;

            var userCategoriesAsString = GetCurrentUserCategoriesAsString(user);

            foreach (var category in allCategories)
            {
                for (int i = 0; i < userCategoriesAsString.Count; i++)
                {
                    if (category.CategoryName == userCategoriesAsString[i])
                    {
                        if (!result.UserCategories.ContainsKey(category.Id))
                        {
                            result.UserCategories.Add(category.Id, true);
                        }
                        else
                        {
                            result.UserCategories[category.Id] = true;
                        }
                        
                    }
                    else
                    {
                        if (!result.UserCategories.ContainsKey(category.Id))
                        {
                            result.UserCategories.Add(category.Id, false);
                        }
                            
                    }
                }
               
            }

            return result;

        }

        private List<string> GetCurrentUserCategoriesAsString(User x)
        {
            List<string> currentUserCategories = new List<string>();
            var userCategoriesIds = dbContext.UserCategories.Where(c => c.UserId == x.Id).Select(x=> x.CategoryId).ToArray();
            var allCategories = dbContext.Categories.ToArray();
            foreach (var category in allCategories)
            {
                foreach (var userCategoryId in userCategoriesIds)
                {
                    if (category.Id == userCategoryId)
                    {

                        currentUserCategories.Add(category.CategoryName);
                        
                    }
                }
            }
            return currentUserCategories;
        }      


        private string GetCurrentUserRole(User x)
        {
            string userRoleId = dbContext.UserRoles.Where(u=> u.UserId == x.Id).FirstOrDefault().RoleId;
            

            return dbContext.Roles.Where(r => r.Id == userRoleId).FirstOrDefault().Name;
        }
    }
}
