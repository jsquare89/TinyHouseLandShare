using Microsoft.AspNetCore.Identity;
using TinyHouseLandshare.Models;

namespace TinyHouseLandshare.Data
{
    public class SeedData
    {
        public static UserEntity tempUser;

        public static async Task InitializeAsync(IServiceProvider services)
        {
            await AddTestUsers(
                services.GetRequiredService<RoleManager<UserRoleEntity>>(),
                services.GetRequiredService<UserManager<UserEntity>>());

            await AddTestData(
                services.GetRequiredService<LandShareDbContext>());
            await AddUserListingForApproval(
                services.GetRequiredService<LandShareDbContext>());
            
        }

        public static async Task AddTestData(
            LandShareDbContext context)
        {
            if (context.SeekerListings.Any() is false)
            {
                CreateSeekerPosts(context);
                await context.SaveChangesAsync();
            }
            if (context.LandListings.Any() is false)
            {
                CreateLandPosts(context);
                await context.SaveChangesAsync();
            }
        }

        private static void CreateLandPosts(LandShareDbContext context)
        {
            context.LandListings.Add(
                new LandListing
                {
                    Title = "Beautiful Acreage Available",
                    Details = "Details about the acreage",
                    Location = "Victoria",
                    CreatedTime = DateTimeOffset.UtcNow,
                    PictureUri = "",
                    MapLocation = "coords go here",
                    Price = "600 per month",
                    AvailableDate = new DateTimeOffset(2022, 04, 1, 0, 0, 0, TimeSpan.Zero),
                    LandType = "Residential",
                    LotSize = "20x40ft 800sqft",
                    FoundationSize = "12x30ft",
                    SiteFoundation = "concrete",
                    DrivewayFoundation = "gravel",
                    WifiConnection = true,
                    WaterConnection = true,
                    ElectricalConnection = true,
                    Parking = true,
                    ChildFriendly = false,
                    Pets = false,
                    SmokingPermitted = false,
                    Privacy = "True",
                    Approved = true,
                    Status = "published",
                    Submitted = true
                });
        }

        private static void CreateSeekerPosts(LandShareDbContext context)
        {
            context.SeekerListings.Add(
                new SeekerListing
                {
                    Title = "Looking for Land",
                    Details = "Details go here",
                    Location = "Langley",
                    CreatedTime = DateTimeOffset.UtcNow,
                    PictureUri = "",
                    HouseSize = "26'x8' 200sqft",
                    OccupantCount = 1,
                    WifiConnectionRequired = true,
                    WaterConnectionRequired = true,
                    ElectricalConnectionRequired = true,
                    PreferedLandType = "Residential",
                    ParkingRequired = true,
                    ChildFriendlyRequired = false,
                    PetsRequired = true,
                    Smoker = false,
                    Privacy = true,
                    Approved = true,
                    Status = "published",
                    Submitted = true
                });

            context.SeekerListings.Add(
                new SeekerListing
                {
                    Title = "Looking for a home for me and my Tiny Mansion",
                    Details = "Details go here lorem ipsem...",
                    Location = "Oliver",
                    CreatedTime = DateTimeOffset.UtcNow,
                    PictureUri = "",
                    HouseSize = "34'x10' 420sqft",
                    OccupantCount = 3,
                    WifiConnectionRequired = true,
                    WaterConnectionRequired = true,
                    ElectricalConnectionRequired = true,
                    PreferedLandType = "Commercial",
                    ParkingRequired = true,
                    ChildFriendlyRequired = true,
                    PetsRequired = true,
                    Smoker = false,
                    Privacy = true,
                    Approved = true,
                    Status = "published",
                    Submitted = true
                });

        }

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
            string userRoleName = "User";
            // Add a test role
            await roleManager.CreateAsync(new UserRoleEntity(adminRoleName));
            await roleManager.CreateAsync(new UserRoleEntity(userRoleName));


            bool adminRoleExists = await roleManager.RoleExistsAsync(adminRoleName);
            if (adminRoleExists)
            {
                string defaultPassword = "Pass123!";

                await CreateUserWithRole(
                    new UserEntity
                    {
                        Email = "admin@landshare.com",
                        UserName = "admin@landshare.com",
                        Name = "Admin",
                        CreatedAt = DateTimeOffset.UtcNow
                    },
                    defaultPassword,
                    adminRoleName,
                    userManager);

                tempUser = await CreateUserWithRole(
                    new UserEntity
                    {
                        Email = "jarredjardine@gmail.com",
                        UserName = "jarredjardine@gmail.com",
                        Name = "Jarred",
                        CreatedAt = DateTimeOffset.UtcNow
                    },
                    defaultPassword,
                    userRoleName,
                    userManager);
            }


        }

        private static async Task AddUserListingForApproval(LandShareDbContext context)
        {
            // Adds seeker listing for temp user
            SeekerListing newSeekerListing = new SeekerListing
            {
                Title = "Tiny House seeking approval lol",
                Details = "Details go here lorem ipsem...",
                Location = "Langley",
                CreatedTime = DateTimeOffset.UtcNow,
                PictureUri = "",
                HouseSize = "34'x10' 420sqft",
                OccupantCount = 3,
                WifiConnectionRequired = true,
                WaterConnectionRequired = true,
                ElectricalConnectionRequired = true,
                PreferedLandType = "Commercial",
                ParkingRequired = true,
                ChildFriendlyRequired = true,
                PetsRequired = true,
                Smoker = false,
                Privacy = true,
                Approved = false,
                Status = "submitted for approval",
                Submitted = true
            };

            context.SeekerListings.Add(newSeekerListing);

            UserSeekerListing userSeekerListing = new UserSeekerListing
            {
                UserId = tempUser.Id,
                SeekerListingId = newSeekerListing.Id
            };
            context.UserSeekerListings.Add(userSeekerListing);
            context.SaveChanges();

            // Adds land listing for temp user
            var newLandListing = new LandListing
                {
                    Title = "Land by the beach",
                    Details = "Details about the land",
                    Location = "Tofino",
                    CreatedTime = DateTimeOffset.UtcNow,
                    PictureUri = "",
                    MapLocation = "coords go here",
                    Price = "800 per month",
                    AvailableDate = new DateTimeOffset(2022, 04, 1, 0, 0, 0, TimeSpan.Zero),
                    LandType = "Rural",
                    LotSize = "20x40ft 800sqft",
                    FoundationSize = "12x30ft",
                    SiteFoundation = "concrete",
                    DrivewayFoundation = "gravel",
                    WifiConnection = true,
                    WaterConnection = true,
                    ElectricalConnection = true,
                    Parking = true,
                    ChildFriendly = true,
                    Pets = true,
                    SmokingPermitted = true,
                    Privacy = "True",
                    Approved = false,
                    Status = "draft",
                    Submitted = true
                };

            context.LandListings.Add(newLandListing);

            var userLandListing = new UserLandListing
            {
                UserId = tempUser.Id,
                LandListingId = newLandListing.Id,
            };
            context.UserLandListings.Add(userLandListing);
            context.SaveChanges();
        }

        private static async Task<UserEntity> CreateUserWithRole(UserEntity user, string password, string role, UserManager<UserEntity> userManager)
        {
            await userManager.CreateAsync(user, password);

            // Put the user in the admin role
            await userManager.AddToRoleAsync(user, role);
            await userManager.UpdateAsync(user);
            return user;
        }
    }
}
