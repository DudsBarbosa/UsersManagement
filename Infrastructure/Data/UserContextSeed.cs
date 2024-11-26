using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public static class UserContextSeed
    {
        public static void Seed(UserContext userContext, ILogger logger)
        {
            // Look for any users
            if (userContext.Users.Any())
            {
                return;
            }

            var users = GetUsersForSeeding();
            foreach (var user in users)
            {
                userContext.Users.Add(user);
            }
            userContext.SaveChanges();
        }

        private static List<User> GetUsersForSeeding()
        {
            return
            [
                new User
                {
                    Name = "John Doe",
                    HourValue = 10.49m,
                    AddDate = DateTime.Now,
                    Active = true
                },
                new User
                {
                    Name = "Jane Doe",
                    HourValue = 15.98m,
                    AddDate = DateTime.Now,
                    Active = true
                }
            ];
        }
    }
}
