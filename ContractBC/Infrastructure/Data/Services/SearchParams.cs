namespace Infrastructure.Data.Services;

public partial class ContractFlexSearchService
{
    public class SearchParams
    {
        public SearchParams(string lastName, string startDate, string endDate)
        {
            LastName = lastName == "" ? null : lastName;
            StartDate = startDate == "" ? null : startDate;
            EndDate = endDate == "" ? null : endDate; ;
        }

        public string LastName { get;  }
        public string StartDate { get;  }
        public string EndDate { get;  }
    }
}
