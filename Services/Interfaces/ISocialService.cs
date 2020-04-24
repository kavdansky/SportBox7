using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Services.Interfaces
{
    public interface ISocialService
    {
        public string SetNewSocialAction (string ip, bool isLiked, int articleId);
    }
}
