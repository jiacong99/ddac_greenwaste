using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DDAC_Assignment.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DDAC_AssignmentUser class
    public class DDAC_AssignmentUser : IdentityUser // modify the table column
    {
        //add extra information such as: name, age, dob, address
        [PersonalData]

        public string User_Full_Name { get; set; }

        [PersonalData]
        public int User_Age { get; set; }

        [PersonalData]
        public DateTime User_DOB { get; set; }

        [PersonalData]
        public string User_Address { get; set; }

        public string User_Role { get; set; }
    }
}
