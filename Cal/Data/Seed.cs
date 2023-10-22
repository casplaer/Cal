using Cal.Models;
using Microsoft.AspNetCore.Identity;

namespace Cal.Data
{
    public class Seed
    {
        public static async Task SeedData(IApplicationBuilder applicationBuilder, ApplicationDbContext context, UserManager<AppUser> userManager)
        {

            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru",
                    Id = context.Users.Count().ToString()
                };
                await userManager.CreateAsync(user, "123456");

                context.SaveChanges();
            }

            if(!context.Categories.Any())
            {
                context.Categories.AddRange(new List<Models.Category>()
                {
                    new Models.Category()
                    {
                        CategoryName = "Тестовая категория",
                        CategoryColor = "#000000",
                    },
                    new Models.Category()
                    {
                        CategoryName = "Тестовая категория 2",
                        CategoryColor = "#d12c7c",
                    },
                    new Models.Category()
                    {
                        CategoryName = "Тестовая категория 3",
                        CategoryColor = "#712cd1",
                    }
                });
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                var user = await userManager.FindByEmailAsync("user@mail.ru");

                var category1 = context.Categories.FirstOrDefault(c => c.CategoryName == "Тестовая категория");
                var category2 = context.Categories.FirstOrDefault(c => c.CategoryName == "Тестовая категория 2");
                var category3 = context.Categories.FirstOrDefault(c => c.CategoryName == "Тестовая категория 3");

                context.Events.AddRange(new List<Models.Event>()
                    {
                        new Models.Event()
                        {
                            Date = new DateTime(2023, 10, 19),
                            Name = "TestEvent1",
                            Description = "TestDescription1",
                            AppUser = user,
                            UserId = user.Id,
                            Category = category1,
                        },
                        new Models.Event()
                        {
                            Date = new DateTime(2023, 10, 20),
                            Name = "TestEvent2",
                            Description = "TestDescription2",
                            AppUser = user,
                            UserId = user.Id,
                            Category = category2,
                        },
                        new Models.Event()
                        {
                            Date = new DateTime(2023, 10, 20),
                            Name = "TestEvent3",
                            Description = "TestDescription3",
                            AppUser = user,
                            UserId = user.Id,
                            Category = category3,
                        }
                    });
                context.SaveChanges();
            }
        }
    }
}
