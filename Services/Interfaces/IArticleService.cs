using SportBox7.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Services.Interfaces
{
    public interface IArticleService
    {
        List<Article> GetArticlesForHomePage();
    }
}
