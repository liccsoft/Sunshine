ALTER TABLE [dbo].[Category]
    ADD CONSTRAINT [Category_ParentCategory] FOREIGN KEY ([ParentCategoryId]) REFERENCES [dbo].[Category] ([CategoryId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

