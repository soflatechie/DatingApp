using System;
using API.Data;
using API.Entities;


namespace API
{
    public class DbInitializer
    {
        public static async Task SeedData(DataContext context)
        {
            var users = new List<AppUser>(){
                new() {
                    Id=1,
                    UserName="Bob"
                },
                new() {
                    Id=2,
                    UserName="Joe"
                },
                new() {
                    Id=3,
                    UserName="Jane"
                }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}

