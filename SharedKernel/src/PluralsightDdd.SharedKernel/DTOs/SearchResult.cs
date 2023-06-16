namespace PublisherSystem.SharedKernel.DTOs;

public class SearchResult
{
    public SearchResult(Guid key, string description,string contractNumber)
    {
        KeyValue = key;
        Description = description;
        ContractNumber = contractNumber;
    }
    private SearchResult()
    {
        
    }
    public Guid KeyValue { get; private set; }
    public string? Description { get; private set; }
    public string? ContractNumber { get; private set; }
}