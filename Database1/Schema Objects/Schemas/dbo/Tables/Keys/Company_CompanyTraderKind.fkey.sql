ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [Company_CompanyTraderKind] FOREIGN KEY ([CompanyTraderKindId]) REFERENCES [dbo].[TradeKinds] ([TraderKindId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

