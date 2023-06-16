using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublisherSystem.SharedKernel.DTOs;
using static Infrastructure.Data.Services.ContractSearchService;

namespace ContractingUI.Pages;

public class IndexModel : PageModel
{
    private readonly ContractSearchService _service;

    [BindProperty(SupportsGet = true)]
    public string? LastNameSearch { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? StartDateSearch { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? EndDateSearch { get; set; }

    public IndexModel(ContractSearchService service)
    {
        _service = service;
    }

    public IList<SearchResult> ContractHighlights { get; set; } = default!;

    public async Task OnPostLastNameSearch()
    {
        ContractHighlights =
         await _service.CallService(ServiceCalls.LastName, new[] { LastNameSearch.Trim() });

    }

    public async Task OnPostDateSearch()
    {
        ContractHighlights =
           await _service.CallService(ContractSearchService.ServiceCalls.DateRange, new[] { StartDateSearch?.Trim(), EndDateSearch?.Trim() });
    }
    public async Task OnPostResetFilter()
    {
        ContractHighlights = await _service.CallService(ServiceCalls.All, new[] { "" });
    }
    public async Task OnGetAsync()
    {

        if (_service.SearchResults == null || Request.QueryString.Value == "?")
        {
            ContractHighlights = await _service.CallService(ServiceCalls.All, new[] { "" });
        }

        else { ContractHighlights = _service.SearchResults; }


    }
}
