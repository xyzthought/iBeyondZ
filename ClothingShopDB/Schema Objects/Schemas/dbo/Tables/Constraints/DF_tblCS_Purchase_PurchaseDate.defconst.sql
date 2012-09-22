ALTER TABLE [dbo].[tblCS_Purchase]
    ADD CONSTRAINT [DF_tblCS_Purchase_PurchaseDate] DEFAULT (getdate()) FOR [PurchaseDate];

