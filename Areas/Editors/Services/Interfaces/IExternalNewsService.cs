using SportBox7.Areas.Editors.ViewModels.Content;
using SportBox7.Areas.Editors.ViewModels.Content.TheSportDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.Services.Interfaces
{
    public interface IExternalNewsService
    {
        public ICollection<RawArticleViewModel> GetExternalNews(int[] permittedCats);

        public RawArticleViewModel GetRawNewsDetails(int id);

        public int MakeRawArticleDraft(int articleId, string userId);

        public Task<LeaguesContainer> GetAllLagues();

        public Task<TeamsContainer> GetAllLagueTeams(int id);

        public Task<Team> GetTeamDetails(int id);


    }
}
