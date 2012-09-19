CREATE TABLE [dbo].[tblCS_SaleDetails] (
    [SaleDetailID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [SaleID]       BIGINT          NULL,
    [ProductID]    INT             NULL,
    [SizeID]       INT             NULL,
    [Quantity]     DECIMAL (18, 2) NULL,
    [Price]        MONEY           NULL
);

