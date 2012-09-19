ALTER TABLE [dbo].[tblCS_Manufacturer]
    ADD CONSTRAINT [DF_tblCS_Manufacturer_CreatedOn] DEFAULT (getdate()) FOR [CreatedOn];

