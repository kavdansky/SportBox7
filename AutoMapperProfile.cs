using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SportBox7.Areas.Editors.ViewModels;
using SportBox7.Data.Models;

namespace SportBox7
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddArticleForReviewViewModel, Article>();
            CreateMap<AddArticleForReviewViewModel, ArticleSeoData>();
        }
    }
}
