using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportBox7.Data.Models;

namespace SportBox7.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleSeoData> ArticlesSeoData { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<League> Leagues { get; set; }

        public DbSet<UserCategory> UserCategories { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<RawArticle> RawArticles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (builder != null)
            {
                builder.Entity<Article>()
                .HasOne(x => x.User)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.CreatorId);

                builder.Entity<Article>()
                    .HasOne(x => x.ArticleSeoData)
                    .WithOne(x => x.Article)
                    .HasForeignKey<ArticleSeoData>(x => x.ArticleId);

                builder.Entity<Article>()
                    .HasMany(x => x.Comments)
                    .WithOne(x => x.Article)
                    .HasForeignKey(x => x.ArticleId);


                builder.Entity<UserCategory>()
                .HasKey(rc => new { rc.CategoryId, rc.UserId });

                builder.Entity<UserCategory>()
                    .HasOne(uc => uc.Category)
                    .WithMany(a => a.UserCategories)
                    .HasForeignKey(rc=> rc.CategoryId);

                builder.Entity<UserCategory>()
                    .HasOne(rc => rc.User)
                    .WithMany(a => a.UserCategories)
                    .HasForeignKey(r=> r.UserId);
            }

            

           



        }
    }
}
