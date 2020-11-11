using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace ApiFrontEnd
{
    public class IndexModel : PageModel
    {
        protected readonly IApiClient _apiClient;

        public List<Question> Questions { get; set; }

        public bool Authenticated { get; set; }

        public MyPageViewModel PageViewModel { get; set; }

        public class MyPageViewModel
        {
            public int PageNumber { get; private set; }
            public int TotalPages { get; private set; }

            public MyPageViewModel(int count, int pageNumber, int pageSize)
            {
                PageNumber = pageNumber;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            }

            public bool HasPreviousPage
            {
                get
                {
                    return (PageNumber > 1);
                }
            }

            public bool HasNextPage
            {
                get
                {
                    return (PageNumber < TotalPages);
                }
            }
        }


        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGet(string? searchText="")
        {

            Questions = await _apiClient.GetQuestionsAsync();
            Questions = Questions.OrderByDescending(x => x.LastActivity).ToList();
            if (!String.IsNullOrEmpty(searchText))
            {
                Questions = Questions.Where(x => x.Topic.Contains(searchText)).ToList();
            }

        }

    }
}
