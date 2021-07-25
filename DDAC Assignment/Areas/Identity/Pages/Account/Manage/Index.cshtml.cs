using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DDAC_Assignment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DDAC_Assignment.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<DDAC_AssignmentUser> _userManager;
        private readonly SignInManager<DDAC_AssignmentUser> _signInManager;

        public IndexModel(
            UserManager<DDAC_AssignmentUser> userManager,
            SignInManager<DDAC_AssignmentUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string Full_Name { get; set; } //lock from edit

            public string User_id { get; set; }

            [Display(Name = "Your Age")]
            [Range(18, 70, ErrorMessage = "Only age between 18 - 70 years old")]
            public int Age { get; set; }

            [Display(Name = "Your Birth Date")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }

            [Display(Name = "Your Home Address")]
            [RegularExpression(@"^[A-Z]+[a-z]*$", ErrorMessage = "Only capital letter and alphabert")]
            public string Address { get; set; }
        }

        private async Task LoadAsync(DDAC_AssignmentUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            
            Input = new InputModel //fetch the data from the table to the form
            {
                PhoneNumber = phoneNumber,
                Full_Name = user.User_Full_Name,
                Age = user.User_Age,
                User_id = user.Id,
                DOB = user.User_DOB,
                Address = user.User_Address
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid) //form data not valid
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if(Input.Address != user.User_Address)
            {
                user.User_Address = Input.Address;
            }
            if (Input.Age != user.User_Age)
            {
                user.User_Age = Input.Age;
            }
            if (Input.DOB != user.User_DOB)
            {
                user.User_DOB = Input.DOB;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
