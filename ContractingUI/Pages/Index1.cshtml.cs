using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublisherSystem.SharedKernel.DTOs;
using static Infrastructure.Data.Services.ContractFlexSearchService;

namespace ContractingUI.Pages
{
    public class Index1Model : PageModel
    {
        private readonly ContractFlexSearchService _service;

        [BindProperty(SupportsGet = true)]
        public string? LastNameSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? StartDateSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? EndDateSearch { get; set; }

        public Index1Model(ContractFlexSearchService service)
        {
            _service = service;
        }

        public IList<SearchResult> ContractHighlights { get; set; } = default!;

        public async Task OnPostFilter()
        {
            var searchParams = new SearchParams(LastNameSearch, StartDateSearch, EndDateSearch);
            ExecuteFilter(searchParams);
        }


        public async Task OnPostResetFilter()
        {
            var searchParams = new SearchParams("", "", "");
            ExecuteFilter(searchParams);
        }



        public async Task OnGetAsync()
        {

            if (_service.SearchResults == null || Request.QueryString.Value == "?")
            {
                var searchParams = new SearchParams("", "", "");
                ExecuteFilter(searchParams);
            }

            else { ContractHighlights = _service.SearchResults; }


        }
        private async Task ExecuteFilter(SearchParams searchParams)
        {
            ContractHighlights =
             await _service.CallService(searchParams);
        }
    }
}

