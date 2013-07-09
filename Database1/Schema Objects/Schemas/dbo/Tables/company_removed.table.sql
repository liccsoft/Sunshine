CREATE TABLE [dbo].[company_removed](
	[CompanyId] [int]  NOT NULL,
	[CompanyName] [nvarchar](256) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[TelNumber] [nvarchar](15) NULL,
	[Mobile] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedUserName] [nvarchar](max) NULL,
	[CompanyTraderKindId] [int] NULL,
	[Payment] [nvarchar](4000) null,
    [RemovedTime] datetime null default(getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


