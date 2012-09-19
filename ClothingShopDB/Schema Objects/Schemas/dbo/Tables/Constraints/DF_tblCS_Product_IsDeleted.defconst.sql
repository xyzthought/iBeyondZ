ALTER TABLE [dbo].[tblCS_Product]
    ADD CONSTRAINT [DF_tblCS_Product_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];

