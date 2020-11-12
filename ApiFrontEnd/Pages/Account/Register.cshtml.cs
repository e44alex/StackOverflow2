using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ApiFrontEnd.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace ApiFrontEnd.Areas.Identity.Pages.Account
{

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public RegisterModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Name")]
            public string Name{ get; set; }

            [Required]
            [Display(Name = "Surname")]
            public string Surname{ get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = Input.Name,
                    Surname = Input.Surname,
                    Login = Input.Email.Substring(0, Input.Email.IndexOf('@')),
                    Email = Input.Email,
                    PasswordHash = Input.Password,
                    DateRegistered = DateTime.Now
                };
                var result = await _apiClient.AddUserAsync(user);
                if (result)
                {
                    string token = await _apiClient.Authenticate(user.Login, Input.Password);
                    if (!string.IsNullOrEmpty(token))
                    {
                        HttpContext.Response.Cookies.Append("token", token.Encrypt());
                        HttpContext.Response.Cookies.Append("user", user.Email);
                        return Redirect("~/");
                    }
                    return Redirect("~/Account/Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User with this Email address has been already registered");
                    return Page();
                }

            }
            // If we got this far, something failed, redisplay form
            return Page();

        }
    }
}
