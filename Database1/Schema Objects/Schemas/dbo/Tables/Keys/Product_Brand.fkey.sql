ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [Product_Brand] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brand] ([BrandId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

