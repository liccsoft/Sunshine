CREATE TABLE [dbo].[LFProduct] (
    [LFProductId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductMark]        NVARCHAR (MAX) NULL,
    [BrandId]            INT            NULL,
    [CategoryId]         INT            NULL,
    [SecondCategoryId]   INT            NULL,
    [ProductDescription] NVARCHAR (MAX) NULL,
    [ProductAdditions]   NVARCHAR (MAX) NULL,
    [ProductNum]         NVARCHAR (MAX) NULL,
    [UserId]             INT            NOT NULL,
    [Createtime]         DATETIME       NOT NULL,
    [Updatetime]         DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([LFProductId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

