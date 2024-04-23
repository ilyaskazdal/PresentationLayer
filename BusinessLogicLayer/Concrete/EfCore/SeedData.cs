using DataAccessLayer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            if(context != null) 
            {
             
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { CategoryName = "Ev"},
                        new Category { CategoryName = "İş" },
                        new Category { CategoryName = "Okul" }
                        );
                    context.SaveChanges();
                }
                if (!context.ProductPosts.Any())
                {
                    context.ProductPosts.AddRange(
                        new ProductPost
                        {
                            ProductPostName = "Mobilya",
                            ProductPostDesccription = "Sandalye Takımı",
                            StillOnSale = true,
                            DateTime = DateTime.Now,
                            Categories = context.Categories.Take(3).ToList(),
                            UserId = 2,
                            Comments = new List<Comment> { 
                                new Comment { CommentText ="Çok kaliteli malzeme", CommentPublishDate = new DateTime(),UserId = 1 },
                                new Comment { CommentText ="Ayakları çok sağlam", CommentPublishDate = new DateTime(),UserId = 2 }
                            }

                        },
                         new ProductPost
                         {
                             ProductPostName = "Klima",
                             ProductPostDesccription = "12m Soğutucu",
                             StillOnSale = true,
                             DateTime = DateTime.Now,
                             Categories = context.Categories.Take(2).ToList(),
                             UserId = 3,

                         }
                        
                        );
                    context.SaveChanges();
                }

            }
        }
    }
}
