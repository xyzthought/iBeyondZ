CREATE TABLE [dbo].[tblCS_AppUserLogin] (
    [UserID]               INT            IDENTITY (1, 1) NOT NULL,
    [UserTypeID]           INT            NULL,
    [FirstName]            NVARCHAR (150) NULL,
    [LastName]             NVARCHAR (150) NULL,
    [LoginID]              NVARCHAR (150) NULL,
    [LoginPassword]        NVARCHAR (150) NULL,
    [CommunicationEmailID] NVARCHAR (150) NULL,
    [LastLoggedIn]         DATETIME       NULL,
    [CreatedOn]            DATE           NULL,
    [UpdatedOn]            DATE           NULL,
    [IsActive]             BIT            NULL,
    [IsDeleted]            BIT            NULL
);

