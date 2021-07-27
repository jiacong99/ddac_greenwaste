using DDAC_Assignment.Areas.Identity.Data;
using DDAC_Assignment.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class MyIdentityDataInitializer
{
    public static void SeedData
(UserManager<DDAC_AssignmentUser> userManager)
    {
        SeedUsers(userManager);
    }

    public async static void SeedUsers
(UserManager<DDAC_AssignmentUser> userManager)
    {

        if (userManager.FindByNameAsync
        ("admin@gmail.com").Result == null)
        {
            DDAC_AssignmentUser user = new DDAC_AssignmentUser()
            {
                User_Role = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                User_Full_Name = "Admin",
                User_DOB = new DateTime(1960, 1, 1),
                EmailConfirmed = true
            };
            var role = Roles.Admin.ToString();
            IdentityResult result = userManager.CreateAsync
            (user, "Admin@123").Result;
            await userManager.AddToRoleAsync(user, role);
        }

        if (userManager.FindByNameAsync
        ("driver@gmail.com").Result == null)
        {
            DDAC_AssignmentUser user = new DDAC_AssignmentUser()
            {
                User_Role = "Driver",
                UserName = "driver@gmail.com",
                Email = "driver@gmail.com",
                User_Full_Name = "Driver",
                User_DOB = new DateTime(1960, 1, 1),
                EmailConfirmed = true
            };
            var role = Roles.Admin.ToString();
            IdentityResult result = userManager.CreateAsync
            (user, "Driver@123").Result;
            await userManager.AddToRoleAsync(user, role);
        }

    }

    internal static void SeedData(object userManager)
    {
        throw new NotImplementedException();
    }
}
