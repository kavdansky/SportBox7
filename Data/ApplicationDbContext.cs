using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportBox7.Data.Models;

namespace SportBox7.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleSeoData> ArticlesSeoData { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<League> Leagues { get; set; }

        public DbSet<RoleCategory> RolesCategories { get; set; }

        public DbSet<Category> Categories { get; set; }


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


                builder.Entity<RoleCategory>()
                .HasKey(rc => new { rc.CategoryId, rc.RoleId });

                builder.Entity<RoleCategory>()
                    .HasOne(rc => rc.Category)
                    .WithMany(a => a.RoleCategories)
                    .HasForeignKey(rc=> rc.CategoryId);

                builder.Entity<RoleCategory>()
                    .HasOne(rc => rc.Role)
                    .WithMany(a => a.RolesCategories)
                    .HasForeignKey(r=> r.RoleId);

                

            }

            

           



        }
    }
}
