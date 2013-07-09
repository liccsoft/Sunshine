CREATE TABLE [dbo].[ProductPriceType] (
    [ProductPriceTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    [DisplayName]        NVARCHAR (MAX) NULL,
    [Name]               NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ProductPriceTypeId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

