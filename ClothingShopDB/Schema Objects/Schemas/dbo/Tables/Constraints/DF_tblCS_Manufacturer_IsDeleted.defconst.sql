ALTER TABLE [dbo].[tblCS_Manufacturer]
    ADD CONSTRAINT [DF_tblCS_Manufacturer_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];

