using Microsoft.AspNetCore.Identity;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestUsers(
                services.GetRequiredService<RoleManager<UserRoleEntity>>(),
                services.GetRequiredService<UserManager<UserEntity>>());

            //await AddTestData(
            //    services.GetRequiredService<DbContext>()));
        }

        //public static async Task AddTestData(
        //    DbContext context)
        //{
        //    if (context.Model.Any())
        //    {
        //        // Already has data
        //        return;
        //    }

        //   // Create models

        //    await context.SaveChangesAsync();
        //}

        private static async Task AddTestUsers(
            RoleManager<UserRoleEntity> roleManager,
            UserManager<UserEntity> userManager)
        {
            var dataExists = roleManager.Roles.Any() || userManager.Users.Any();

            if (dataExists)
            {
                return;
            }

            string adminRoleName = "Admin";
            // Add a test role
            await roleManager.CreateAsync(new UserRoleEntity(adminRoleName));


            bool adminRoleExists = await roleManager.RoleExistsAsync(adminRoleName);
            if (adminRoleExists)
            {
                // Add a test user
                var user = new UserEntity
                {
                    Email = "admin@landshare.com",
                    UserName = "admin@lanshare.com",
                    Name = "Admin",
                    CreatedAt = DateTimeOffset.UtcNow
                };


                await userManager.CreateAsync(user, "Pass123!");

                // Put the user in the admin role
                await userManager.AddToRoleAsync(user, adminRoleName);
                await userManager.UpdateAsync(user);
            }
        }
    }
}
