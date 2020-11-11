using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace ApiFrontEnd.Pages
{
    public class UserModel : PageModel
    {
        private readonly IApiClient _apiClient;
        
        public User ModelUser { get; set; }


        public UserModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGet(string username)
        {
            ModelUser = await _apiClient.GetUserDataAsync(username);
        }
    }
}
