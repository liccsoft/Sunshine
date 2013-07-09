ALTER TABLE [dbo].[LFProduct]
    ADD CONSTRAINT [LFProduct_CategoryMain] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

