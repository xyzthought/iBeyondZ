ALTER TABLE [dbo].[tblCS_AppUserLogin]
    ADD CONSTRAINT [DF_tblCS_AppUserLogin_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];

