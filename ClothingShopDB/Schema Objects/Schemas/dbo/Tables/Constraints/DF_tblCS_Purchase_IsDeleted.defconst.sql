ALTER TABLE [dbo].[tblCS_Purchase]
    ADD CONSTRAINT [DF_tblCS_Purchase_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];

