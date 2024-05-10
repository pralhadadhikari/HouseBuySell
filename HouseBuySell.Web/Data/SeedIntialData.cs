using HouseBuySell.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace HouseBuySell.Web.Data
{
    public class SeedIntialData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] Roles = { "SUPER_ADMIN", "ADMIN", "Broker", "Buyer" };

            foreach (string roleName in Roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (await _userManager.FindByNameAsync("superadmin") == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = "superadmin",
                    Email = "sueradmin@mail.com",
                    PhoneNumber = "1122334455",
                    Address = "Kathmandu",
                    Name = "superadmin",
                    OfficeName = "superadmin",
                    OfficeAddress = "superadmin",
                    OfficeNumber = "99887755",
                    IsActive = true,
                    CreatedBy = "superadmin",
                    CreatedDate = DateTime.Now
                };

                var res = await _userManager.CreateAsync(user, "Super@dmin1");
                await _userManager.SetLockoutEnabledAsync(user, false);
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "SUPER_ADMIN");
                }
            }


        }



        //public static async Task InitializeAsync
        //    (RoleManager<IdentityRole> roleManager,
        //    UserManager<ApplicationUser> userManager)
        //{

        //    string[] Roles = { "SUPER_ADMIN", "ADMIN", "Broker", "Buyer" };

        //    foreach (string roleName in Roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(roleName))
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }

        //    if (await userManager.FindByNameAsync("superadmin") == null)
        //    {
        //        var user = new ApplicationUser()
        //        {
        //            UserName = "superadmin",
        //            Email = "sueradmin@mail.com",
        //            PhoneNumber = "1122334455",
        //            Address = "Kathmandu",
        //            Name="",
        //            OfficeName="",
        //            OfficeAddress="",
        //            OfficeNumber="",
        //            IsActive=true,
        //            CreatedBy = "superadmin",
        //            CreatedDate=DateTime.Now
        //        };

        //        var res = await userManager.CreateAsync(user, "Super@dmin1");
        //        await userManager.SetLockoutEnabledAsync(user, false);
        //        if (res.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(user, "SUPER_ADMIN");
        //        }
        //    }
        //}
    }
}
