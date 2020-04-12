using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Areas.Editors.ViewModels.Content;
using SportBox7.Data.Models;

namespace SportBox7
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddArticleViewModel, Article>();
            CreateMap<AddArticleViewModel, ArticleSeoData>();
            CreateMap<Article, AllArticlesViewModel>();
            CreateMap<Article, EditArticleViewModel>();
            CreateMap<RawArticle, RawArticleViewModel>();
            CreateMap<RawArticleViewModel, RawArticle>();
            CreateMap<RawArticle, Article>();
         
        }
    }
}
