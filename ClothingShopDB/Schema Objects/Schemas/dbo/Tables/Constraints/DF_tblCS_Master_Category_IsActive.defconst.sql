ALTER TABLE [dbo].[tblCS_Master_Category]
    ADD CONSTRAINT [DF_tblCS_Master_Category_IsActive] DEFAULT ((1)) FOR [IsActive];

