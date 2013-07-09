ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [Product_CategorySecond] FOREIGN KEY ([SecondCategoryId]) REFERENCES [dbo].[Category] ([CategoryId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

