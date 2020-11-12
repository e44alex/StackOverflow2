using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiFrontEnd.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace ApiFrontEnd.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {

        private readonly IApiClient _apiClient;
        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
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

            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Surname")]
            public string Surname { get; set; }

            [Display(Name = "Login")]
            public string Login { get; set; }

            [Display(Name = "Rating")]
            public float? Rating { get; set; }

            public string Position { get; set; }
            public float? Experience{ get; set; }
            public string Bio{ get; set; }
        }



        private async Task LoadAsync(User user)
        {
            
            Input = new InputModel
            {
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname,
                Login = user.Login,
                Rating = user.Rating, 
                Bio = user.Bio,
                Experience = user.Exerience, 
                Position = user.Position
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Username = HttpContext.Request.Cookies["user"];
            var user = await _apiClient.GetUserDataAsync(Username);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_apiClient.GetUserIdAsync(Username)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _apiClient.GetUserDataAsync(HttpContext.Request.Cookies["user"]);
            if (user == null)
            {
                return NotFound($"Unable to load user ");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.Name = Input.Name;
            user.Surname = Input.Surname;
            user.PhoneNumber = Input.PhoneNumber;
            user.Login = Input.Login;
            user.Bio = Input.Bio;
            user.Position = Input.Position;
            user.Exerience= Input.Experience;

            await _apiClient.UpdateUserAsync(user, HttpContext.Request.Cookies["token"].Decrypt());

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
