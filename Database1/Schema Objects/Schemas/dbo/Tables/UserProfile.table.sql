CREATE TABLE [dbo].[UserProfile] (
    [UserProfileId] INT            IDENTITY (1, 1) NOT NULL,
    [NickName]      NVARCHAR (256) NULL,
    [Email]         NVARCHAR (256) NULL,
    [QQ]            NVARCHAR (256) NULL,
    [Weibo]         NVARCHAR (256) NULL,
    [Address]       NVARCHAR (256) NULL,
    [Tel]           NVARCHAR (32)  NULL,
    [Mobile]        NVARCHAR (32)  NULL,
    PRIMARY KEY CLUSTERED ([UserProfileId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

