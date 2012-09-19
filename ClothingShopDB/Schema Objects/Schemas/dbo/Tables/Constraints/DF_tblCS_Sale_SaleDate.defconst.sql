ALTER TABLE [dbo].[tblCS_Sale]
    ADD CONSTRAINT [DF_tblCS_Sale_SaleDate] DEFAULT (getdate()) FOR [SaleDate];

