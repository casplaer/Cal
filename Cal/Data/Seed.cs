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
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange(new List<Models.Event>()
                    {
                        new Models.Event()
                        {
                            Date = new DateTime(2023, 9, 19),
                            Name = "TestEvent1",
                            Description = "TestDescription1",
                            AppUser = await context.Users.FindAsync("1"),
                            UserId = "1"
                        },
                        new Models.Event()
                        {
                            Date = new DateTime(2023, 9, 20),
                            Name = "TestEvent2",
                            Description = "TestDescription2",
                            AppUser = await context.Users.FindAsync("1"),
                            UserId = "1"
                        },
                        new Models.Event()
                        {
                            Date = new DateTime(2023, 9, 20),
                            Name = "TestEvent3",
                            Description = "TestDescription3",
                            AppUser = await context.Users.FindAsync("1"),
                            UserId = "1"
                        }
                    });
                context.SaveChanges();
            }
        }
    }
}
