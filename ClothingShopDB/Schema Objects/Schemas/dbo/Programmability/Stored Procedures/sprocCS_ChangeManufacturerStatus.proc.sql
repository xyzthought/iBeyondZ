/*-- =============================================
-- Created by: Biswajit
-- Created on: 25-Aug-2012
-- Purpose: Change Status of Manufacturer Active/inActive
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
exec sprocCS_ChangeManufacturerStatus 1, 0
-- =============================================*/

CREATE PROCEDURE dbo.sprocCS_ChangeManufacturerStatus
	@ManufacturerID INT
	, @IsActive BIT
AS
	UPDATE
		[dbo].[tblCS_Manufacturer]
	SET
		IsActive = @IsActive
	WHERE
		ManufacturerID = @ManufacturerID
	
