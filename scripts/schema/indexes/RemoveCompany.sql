use $(DBName)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	select * from Company where CompanyId = @CompanyId;
	
	delete from Company where CompanyId = @CompanyId;
	commit tran
END
GO
