CREATE TABLE [dbo].[tblCS_Product] (
    [ProductID]      INT             IDENTITY (1, 1) NOT NULL,
    [ProductName]    NVARCHAR (500)  NULL,
    [Description]    NVARCHAR (MAX)  NULL,
    [ManufacturerID] INT             NULL,
    [CategoryID]     INT             NULL,
    [SizeID]         INT             NULL,
    [BuyingPrice]    MONEY           NULL,
    [Tax]            MONEY           NULL,
    [Margin]         DECIMAL (18, 2) NULL,
    [BarCode]        NVARCHAR (500)  NULL,
    [CreatedOn]      DATE            NULL,
    [UpdatedOn]      DATE            NULL,
    [CreatedBy]      INT             NULL,
    [UpdatedBy]      INT             NULL,
    [IsActive]       BIT             NULL,
    [IsDeleted]      BIT             NULL
);

