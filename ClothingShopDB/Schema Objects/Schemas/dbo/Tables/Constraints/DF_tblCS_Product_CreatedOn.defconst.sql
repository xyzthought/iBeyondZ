ALTER TABLE [dbo].[tblCS_Product]
    ADD CONSTRAINT [DF_tblCS_Product_CreatedOn] DEFAULT (getdate()) FOR [CreatedOn];

