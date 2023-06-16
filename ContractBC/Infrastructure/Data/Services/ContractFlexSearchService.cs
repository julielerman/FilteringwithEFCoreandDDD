using Microsoft.EntityFrameworkCore;
using PublisherSystem.SharedKernel.DTOs;

namespace Infrastructure.Data.Services;

public partial class ContractFlexSearchService
{
    SearchContext _context;
    static List<SearchResult> _results;

    public ContractFlexSearchService(SearchContext context)
    {
        _context = context;
    }
    public List<SearchResult> SearchResults => _results;

    public async Task<List<SearchResult>> CallService(SearchParams sP)
    {
        _results = _context.SearchResults
            .FromSqlInterpolated($"GetContractsFlexTempTable {sP.LastName},{sP.StartDate},{sP.EndDate}").ToList();
        return _results;
    }
}
