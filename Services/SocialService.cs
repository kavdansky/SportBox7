using SportBox7.Data;
using SportBox7.Data.Models;
using SportBox7.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Web;
using Newtonsoft.Json;

namespace SportBox7.Services
{
    public class SocialService : ISocialService
    {
        private readonly ApplicationDbContext dbContext;

        public SocialService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string SetNewSocialAction(string ip, bool isLiked, int articleId)
        {
            if (!dbContext.SocialSignals.Where(s=> s.ArticleId == articleId && s.VisitorIp == ip).Any())
            {
                SocialSignal socialSignal = new SocialSignal() { ArticleId = articleId, IsLiked = isLiked, VisitorIp = ip };
                dbContext.SocialSignals.Add(socialSignal);
                dbContext.SaveChanges();
            }
            
            int likes = dbContext.SocialSignals.Where(s => s.ArticleId == articleId && s.IsLiked == true).Count();
            int dislikes = dbContext.SocialSignals.Where(s => s.ArticleId == articleId && s.IsLiked == false).Count();
            string result = likes + "!" + dislikes;
            return result;
        }


    }

    public class LikesData
    {
        public int Likes { get; set; }
        public int Dislikes { get; set; }

    }
}
