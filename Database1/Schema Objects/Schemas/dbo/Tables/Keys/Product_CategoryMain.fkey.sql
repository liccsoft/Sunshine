ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [Product_CategoryMain] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

