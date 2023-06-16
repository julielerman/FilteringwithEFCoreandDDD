USE [ContractUI]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[GetContractsFlexTempTable]
		@LastName = NULL,
		@initdatestart = N'05/20/2023',
		@initdateend = NULL

SELECT	@return_value as 'Return Value'

GO
PRINT COALESCE(NULL, GETDATE()+1)