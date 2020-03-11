using SportBox7.Data;
using SportBox7.Data.Models;
using SportBox7.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext dbContext;

        public ArticleService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        

        }


        public List<Article> GetArticlesForHomePage()
        {
            return dbContext.Articles.OrderByDescending(x => x.CreationDate).Take(7).ToList();
                
        }
    }
}
