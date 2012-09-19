ALTER TABLE [dbo].[tblCS_Product]
    ADD CONSTRAINT [DF_tblCS_Product_IsActive] DEFAULT ((1)) FOR [IsActive];

