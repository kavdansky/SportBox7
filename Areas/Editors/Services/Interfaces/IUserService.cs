using SportBox7.Areas.Editors.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IUserService
    {
        public List<AllUsersViewModel> GetAllUsers();

        public DeactivateUserViewModel GetUser(string id);

        public bool DeactivateUser(DeactivateUserViewModel model);

        public bool ActivateUser(DeactivateUserViewModel model);

        public EditUserViewModel GetUserToEdit(string userId);

        public Task<bool> EditUserAsync(EditUserViewModel model);
    }
}
