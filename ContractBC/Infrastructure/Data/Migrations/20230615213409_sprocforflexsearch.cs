using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class sprocforflexsearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"



CREATE PROCEDURE GetContractsFlexTempTable
    @LastName varchar(15),
	@initdatestart varchar(20),
	@initdateend varchar(20)
	
AS

DECLARE @SDate DATETIME
DECLARE @EDate DATETIME

SET @SDate = ISNULL(@initdatestart, '19000101')
SET @EDate = ISNULL(@initdateend, GETDATE()+100)


select currentversionid into #ContractSubSet from CurrentContractversions WHERE 1=2

BEGIN
IF @LastName IS NOT NULL
  BEGIN
  if (@initdatestart is null and @initdateend is null) 
  --only filtering on lastname
		 INSERT  INTO #ContractSubSet
         select currentversionid from currentcontractversions
         where left(LastName,len(trim(@LastName)))=trim(@LastName) ;
  
  ELSE if (@initdatestart is not null or @initdateend is not null)
  -- filtering on lastname and date time
       INSERT  INTO #ContractSubSet
	   select currentversionid 
       from currentcontractversions
       where left(LastName,len(trim(@LastName)))=trim(@LastName) 
	   AND dateinitiated >=@SDate
	   AND dateinitiated <=@EDate;
  
  END
ELSE
  --only filtering on date
  if (@initdatestart is not null or @initdateend is not null)
    
     INSERT  INTO #ContractSubSet
     select currentversionid 
     from currentcontractversions
     where dateinitiated >=@SDate
	   AND dateinitiated <=@EDate;
	 
ELSE
--no filter
    INSERT  INTO #ContractSubSet
     select currentversionid 
     from currentcontractversions;
END
--now build and group the result set for the values in the subset
select groupednames.contractId as KeyValue,[description],ContractNumber
FROM
    (select contractid,currentversionid, dbo.BuildContractHighlights( dateinitiated,workingtitle,string_agg(FirstName + ' ' + LastName,',')) as [description],ContractNumber 
    from CurrentContractversions
    where currentversionid in 
        (select currentversionid from #ContractSubSet)
    group by CurrentVersionId,WorkingTitle,contractid,DateInitiated,ContractNumber)  groupednames
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP GetContractsFlexTempTable");
        }
    }
}
