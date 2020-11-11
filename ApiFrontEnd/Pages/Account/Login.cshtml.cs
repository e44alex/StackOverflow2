using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiFrontEnd.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackOverflowWebApi.Services;

namespace StackOverflow.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IApiClient _apiClient;
        public LoginModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [DisplayName("Email")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            string token = await _apiClient.Authenticate(Input.Username, Input.Password);
            if (!string.IsNullOrEmpty(token))
            {
                
                HttpContext.Response.Cookies.Append("token", token.Encrypt());
                HttpContext.Response.Cookies.Append("user", Input.Username);
                return Redirect("~/");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();

        }


    }
}
