using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class sproctosearchall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
    @"CREATE PROCEDURE GetContractHighlightsAll
   
AS
select groupednames.contractId as KeyValue,[description],ContractNumber
FROM
    (select contractid,currentversionid, dbo.BuildContractHighlights( dateinitiated,workingtitle,string_agg(FirstName + ' ' + LastName,',')) as [description],ContractNumber 
    from CurrentContractversions
    where currentversionid in 
        (select currentversionid
         from currentcontractversions)
    group by CurrentVersionId,WorkingTitle,contractid,DateInitiated,ContractNumber)  groupednames
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE GetCOntractHighlightsAll");
            
        }
    }
}
