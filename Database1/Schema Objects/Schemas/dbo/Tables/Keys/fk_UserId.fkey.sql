ALTER TABLE [dbo].[webpages_UsersInRoles]
    ADD CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

