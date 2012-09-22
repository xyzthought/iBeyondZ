CREATE TABLE [dbo].[tblCS_Sale] (
    [SaleID]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [CustomerID]     INT           NULL,
    [PaymentModeID]  INT           NULL,
    [CCNumber]       NVARCHAR (50) NULL,
    [CVVNumber]      NVARCHAR (50) NULL,
    [ExpiredOn]      VARCHAR (10)  NULL,
    [SaleDate]       DATETIME      NULL,
    [StandardRebate] MONEY         NULL,
    [Discount]       MONEY         NULL,
    [SaleMadeBy]     INT           NULL
);

