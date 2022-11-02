using Microsoft.AspNetCore.Identity;

namespace CSE_Clubhouse.Areas.Identity.Data
{
	public static class ContextSeed
	{
        public static async Task SeedRolesAsync(UserManager<ClubhouseUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.ClubModerator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ClubhouseUser> userManager)
        {
            //Seed Default User
            var defaultUser = new ClubhouseUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FirstName = "Abhishek",
                LastName = "Kayasth",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin@025");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.ClubModerator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.User.ToString());
                }

            }
        }
    }
}
