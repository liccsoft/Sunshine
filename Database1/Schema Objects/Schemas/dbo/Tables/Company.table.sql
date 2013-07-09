CREATE TABLE [dbo].[Company] (
    [CompanyId]           INT             IDENTITY (1, 1) NOT NULL,
    [CompanyName]         NVARCHAR (256)  NOT NULL,
    [Address]             NVARCHAR (MAX)  NULL,
    [TelNumber]           NVARCHAR (15)   NULL,
    [Mobile]              NVARCHAR (MAX)  NULL,
    [Description]         NVARCHAR (MAX)  NULL,
    [CreatedUserName]     NVARCHAR (MAX)  NULL,
    [CompanyTraderKindId] INT             NULL,
    [Payment]             NVARCHAR (4000) NULL,
    PRIMARY KEY CLUSTERED ([CompanyId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

