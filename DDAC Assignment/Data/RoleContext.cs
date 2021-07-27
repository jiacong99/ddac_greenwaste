using DDAC_Assignment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_Assignment.Areas.Identity.Data
{
    public enum Roles
    {
        Admin,
        Driver,
        Customer
    }
    public class RoleContext
    {
        public static async Task SeedRolesAsync(UserManager<DDAC_AssignmentUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Driver.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));
 
        }
    }
}
