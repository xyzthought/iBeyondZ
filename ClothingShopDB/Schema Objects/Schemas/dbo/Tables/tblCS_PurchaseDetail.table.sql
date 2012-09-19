CREATE TABLE [dbo].[tblCS_PurchaseDetail] (
    [PurchaseDetailID] BIGINT IDENTITY (1, 1) NOT NULL,
    [PurchaseID]       INT    NOT NULL,
    [ProductID]        INT    NULL,
    [SizeID]           INT    NULL,
    [Quantity]         INT    NULL,
    [Price]            MONEY  NULL
);

