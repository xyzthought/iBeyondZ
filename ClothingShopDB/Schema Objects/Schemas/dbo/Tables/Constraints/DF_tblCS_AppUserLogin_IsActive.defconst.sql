ALTER TABLE [dbo].[tblCS_AppUserLogin]
    ADD CONSTRAINT [DF_tblCS_AppUserLogin_IsActive] DEFAULT ((1)) FOR [IsActive];

