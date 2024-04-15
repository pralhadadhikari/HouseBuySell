using HouseBuySell.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace HouseBuySell.Web.Data
{
    public class SeedIntialData
    {
        public static async Task Initialize
            (RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {

            string[] Roles = { "SUPER_ADMIN", "ADMIN", "Broker", "Buyer" };

            foreach (string roleName in Roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (await userManager.FindByNameAsync("superadmin") == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = "superadmin",
                    Email = "sueradmin@mail.com",
                    PhoneNumber = "1122334455",
                    Address = "Kathmandu",
                    Name="",
                    OfficeName="",
                    OfficeAddress="",
                    OfficeNumber="",
                    IsActive=true,
                    CreatedBy = "superadmin",
                    CreatedDate=DateTime.Now
                };

                var res = await userManager.CreateAsync(user, "Super@dmin1");
                await userManager.SetLockoutEnabledAsync(user, false);
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "SUPER_ADMIN");
                }
            }
        }
    }
}
