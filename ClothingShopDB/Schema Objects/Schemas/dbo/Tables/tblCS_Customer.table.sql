CREATE TABLE [dbo].[tblCS_Customer] (
    [CustomerID] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (150) NULL,
    [LastName]   NVARCHAR (150) NULL,
    [Address]    NVARCHAR (MAX) NULL,
    [City]       NVARCHAR (150) NULL,
    [ZIP]        NVARCHAR (50)  NULL,
    [Country]    NVARCHAR (150) NULL,
    [TeleNumber] NVARCHAR (150) NULL,
    [Email]      NVARCHAR (150) NULL,
    [CreatedOn]  DATETIME       NULL,
    [CreatedBy]  INT            NULL
);

