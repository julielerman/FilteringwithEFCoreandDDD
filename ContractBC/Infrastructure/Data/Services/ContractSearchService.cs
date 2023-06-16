using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PublisherSystem.SharedKernel.DTOs;

namespace Infrastructure.Data.Services;

public class ContractSearchService
{
    SearchContext _context;
   static List<SearchResult> _results;

    public ContractSearchService(SearchContext context)
    {
        _context = context;
    }
    public  List<SearchResult> SearchResults => _results;
   public async Task<List<SearchResult>> CallService(ServiceCalls serviceCall, string[] filter)
    {
        switch (serviceCall)
        {
          
            case ServiceCalls.LastName: return await GetContractPickListForAuthorLastName(filter[0]);
          
            case ServiceCalls.DateRange:
                DateTime startdate;
                if (string.IsNullOrEmpty(filter[0]))
                    { startdate = DateTime.MinValue; }
                else {
                    DateTime.TryParse(filter[0], out startdate);
                }
                DateTime enddate;
                if (string.IsNullOrEmpty(filter[1]))
                { enddate = DateTime.MaxValue; }
                else
                {
                    DateTime.TryParse(filter[1], out enddate);
                }
                return await GetContractPickListForInitiatedDateRange(startdate, enddate);
           
            default: return await GetContractPickListForAll();
        }
        
    }

   
    private async Task<List<SearchResult>> GetContractPickListForAll()
    {
        _results = _context.SearchResults.FromSqlInterpolated
          ($"GetContractHighlightsAll").ToList();
        return _results;
    }
    private async Task<List<SearchResult>> 
        GetContractPickListForAuthorLastName(string lastnameStart)
    {
        _results=  _context.SearchResults.FromSqlInterpolated
            ($"GetContractsForAuthorLastNameStartswith {lastnameStart}").ToList();
        return _results;
    }

    private async Task<List<SearchResult>> 
        GetContractPickListForInitiatedDateRange(DateTime datestart, DateTime dateend)
    {
        _results= _context.SearchResults.FromSqlInterpolated
            ($"GetContractsInitiatedInDateRange {datestart},{dateend}").ToList();
        return _results;

    }
    //other options 
    //all contracts? Unsigned contracts? Abandoned contracts?

    public enum ServiceCalls
    {
        LastName,
        DateRange,
        All

    }  
}
