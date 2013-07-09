CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

