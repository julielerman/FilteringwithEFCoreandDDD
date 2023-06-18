# Applying DDD Thinking to EF Core Search Filters!

This is a twist on the solution in [EF Core 6 and DDD Pluralsight course repo](https://github.com/julielerman/EFCore6andDDDPluralsight).  

Here, I have added a more intelligent search and a UI (a pair in fact) for the dotnet [FWDays '23](https://fwdays.com/en/event/dotnet-fwdays-2023) presentation, _Applying DDD Thinking to EF Core Search Filters_

![Snag_8f7ca59](https://github.com/julielerman/FilteringwithEFCoreandDDD/assets/5007120/fd337688-c40c-4d5f-86a7-757e7e5324d4)

**The two branches** 
1) [UsingASearchContext](https://github.com/julielerman/FilteringwithEFCoreandDDD/tree/UsingASearchContext) has all of the extra solution logic from the course such as unit tests, integration tests, event handlers etc.
2) [SearchContextLite](https://github.com/julielerman/FilteringwithEFCoreandDDD/tree/searchcontextlite) is trimmed down with only the necessary logic to focus on the search demo.

**Two ways to filter** 
1) The first is via the Home page  and lets you perform one filter at a time. Those filters eventually call individual stored procedures.
2) The second is via the Flex page and lets you combine filters and leans on specification pattern via a search class. That filtering leads to a single , complex stored proc but the code in the ContractSearchFlexService is incredibly simple as a result.

**Creating the database**
The application startup will check for the existence of the database (demo uses SQL Server localdb). The db name and connection string are in appsettings of the UI project.
If there is no database or if it exists with zero rows in the Contracts table, it will recreate the database and seed it with data for 7 contracts. Note that creating or migrating a database in startup is not recommended for production apps that could have multiple instances. But for the purpose of this demo, it's perfectly ok to do. Just sayin' ....
