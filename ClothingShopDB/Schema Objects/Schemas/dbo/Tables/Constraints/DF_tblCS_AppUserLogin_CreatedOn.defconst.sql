ALTER TABLE [dbo].[tblCS_AppUserLogin]
    ADD CONSTRAINT [DF_tblCS_AppUserLogin_CreatedOn] DEFAULT (getdate()) FOR [CreatedOn];

