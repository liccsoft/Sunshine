
use $(DBName)
GO
/****** Object:  Index [idx_company_companyname]    Script Date: 06/29/2013 08:40:45 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND name = N'idx_company_companyname')
DROP INDEX [idx_company_companyname] ON [dbo].[Company] WITH ( ONLINE = OFF )
GO

/****** Object:  Index [idx_company_companyname]    Script Date: 06/29/2013 08:40:46 ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_company_companyname] ON [dbo].[Company] 
(
	[CompanyName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


