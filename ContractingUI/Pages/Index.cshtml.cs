using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContractBC.ContractAggregate;
using Infrastructure.Data.Services;
using PublisherSystem.SharedKernel.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContractingUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ContractSearchService _service;

        [BindProperty(SupportsGet = true)]
        public string? LastNameSearch { get; set; }

        //public SelectList? LastNames { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StartDateSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? EndDateSearch { get; set; }

        public IndexModel(ContractSearchService service)
        {
            _service = service;
        }

        public IList<SearchResult> ContractHighlights { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_service.SearchResults == null || Request.QueryString.Value=="?")
            {
                ContractHighlights = await _service.CallService(ContractSearchService.ServiceCalls.All,new[] { "" });
            }

            if (!string.IsNullOrEmpty(LastNameSearch))
            {
                ContractHighlights = 
                    await _service.CallService(ContractSearchService.ServiceCalls.LastName, new[] { LastNameSearch.Trim() });

            }

            if (!string.IsNullOrEmpty(StartDateSearch) || !string.IsNullOrEmpty(EndDateSearch))
            {
                ContractHighlights =
                    await _service.CallService(ContractSearchService.ServiceCalls.DateRange,new[] { StartDateSearch?.Trim(), EndDateSearch?.Trim() });

            }
            else { ContractHighlights=_service.SearchResults; }


        }
    }
}
