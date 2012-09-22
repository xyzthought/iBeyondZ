CREATE TABLE [dbo].[tblCS_Purchase] (
    [PurchaseID]     INT      IDENTITY (1, 1) NOT NULL,
    [ManufacturerID] INT      NULL,
    [PurchaseDate]   DATETIME NULL,
    [IsDeleted]      BIT      NOT NULL
);

