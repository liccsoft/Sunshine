ALTER TABLE [dbo].[LFProduct]
    ADD CONSTRAINT [LFProduct_CategorySecond] FOREIGN KEY ([SecondCategoryId]) REFERENCES [dbo].[Category] ([CategoryId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

