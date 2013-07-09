-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE RemoveCompany 
	@CompanyId int

AS
BEGIN
	SET NOCOUNT ON;
	set xact_abort on
	begin tran
	update [User] set CompanyId = null, CompanyStatus = null where CompanyId = @CompanyId;
	
	insert into Company_removed
	select *, GETDATE() from Company where CompanyId = @CompanyId;
	
	delete from Company where CompanyId = @CompanyId;
	commit tran
END


