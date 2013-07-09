CREATE TABLE [dbo].[Category] (
    [CategoryId]       INT            IDENTITY (1, 1) NOT NULL,
    [ParentCategoryId] INT            NULL,
    [CategoryLevel]    INT            NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    [CategoryName]     NVARCHAR (MAX) NULL,
    [Title]            NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([CategoryId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

