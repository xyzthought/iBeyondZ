/*-- =============================================
-- Created by: Biswajit
-- Created on: 25-Aug-2012
-- Purpose: Returns Manufacturer details as per Supplied ID
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
exec sprocCS_GetManufacturerByID 1
-- =============================================*/

CREATE PROCEDURE dbo.sprocCS_GetManufacturerByID
	@ManufacturerID INT
AS
	SELECT [ManufacturerID]
      ,[CompanyName]
      ,[ContactFirstName]
      ,[ContactLastName]
      ,[Address]
      ,[ZIP]
      ,[City]
      ,[Country]
      ,[Phone]
      ,[Email]
      ,[IsActive]
	FROM 
		[dbo].[tblCS_Manufacturer]
	WHERE
		ManufacturerID = @ManufacturerID
  
