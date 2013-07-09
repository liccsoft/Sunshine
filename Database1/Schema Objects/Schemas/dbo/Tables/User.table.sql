CREATE TABLE [dbo].[User] (
    [UserId]        INT            IDENTITY (1, 1) NOT NULL,
    [UserName]      NVARCHAR (128) NOT NULL,
    [UserProfileId] INT            NULL,
    [CompanyId]     INT            NULL,
    [ProfileStatus] INT            NULL,
    [CompanyStatus] INT            NULL,
    [IsAdmin]       BIT            NOT NULL,
    [CreationTime]  DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

