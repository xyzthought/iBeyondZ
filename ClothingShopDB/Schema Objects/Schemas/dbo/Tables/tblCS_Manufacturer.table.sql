CREATE TABLE [dbo].[tblCS_Manufacturer] (
    [ManufacturerID]   INT             IDENTITY (1, 1) NOT NULL,
    [CompanyName]      NVARCHAR (250)  NULL,
    [ContactFirstName] NVARCHAR (150)  NULL,
    [ContactLastName]  NVARCHAR (150)  NULL,
    [Address]          NVARCHAR (1000) NULL,
    [ZIP]              NVARCHAR (50)   NULL,
    [City]             NVARCHAR (150)  NULL,
    [Country]          NVARCHAR (150)  NULL,
    [Phone]            NVARCHAR (150)  NULL,
    [Email]            NVARCHAR (150)  NULL,
    [CreatedOn]        DATE            NULL,
    [UpdatedOn]        DATE            NULL,
    [CreatedBy]        INT             NULL,
    [UpdatedBy]        INT             NULL,
    [IsActive]         BIT             NULL,
    [IsDeleted]        BIT             NULL
);

