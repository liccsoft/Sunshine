CREATE TABLE [dbo].[ProductType] (
    [ProductTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NULL,
    [DisplayName]   NVARCHAR (MAX) NULL,
    [Description]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ProductTypeId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

