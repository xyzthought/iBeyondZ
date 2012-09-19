ALTER TABLE [dbo].[tblCS_Manufacturer]
    ADD CONSTRAINT [DF_tblCS_Manufacturer_IsActive] DEFAULT ((1)) FOR [IsActive];

