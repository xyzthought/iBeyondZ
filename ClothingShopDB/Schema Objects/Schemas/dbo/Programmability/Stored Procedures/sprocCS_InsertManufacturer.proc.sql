/*-- =============================================
-- Created by: Biswajit
-- Created on: 25-Aug-2012
-- Purpose: Create new Manufacturer
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
exec sprocCS_InsertManufacturer
-- =============================================*/

CREATE PROCEDURE dbo.sprocCS_InsertManufacturer
	@CompanyName NVARCHAR(250)
	,@ContactFirstName NVARCHAR(150)
	,@ContactLastName NVARCHAR(150)
	,@Address NVARCHAR(1000)
	,@ZIP NVARCHAR(50)
	,@City NVARCHAR(150)
	,@Country NVARCHAR(150)
	,@Phone NVARCHAR(150)
	,@Email NVARCHAR(150)
	,@CreatedBy INT
	,@MessageID INT OUT
	,@Message NVARCHAR(255) OUT
AS
	BEGIN TRY
	
		IF NOT EXISTS(SELECT ManufacturerID FROM dbo.tblCS_Manufacturer 
						WHERE CompanyName=@CompanyName AND IsDeleted = 0)
		BEGIN
			INSERT INTO [dbo].[tblCS_Manufacturer]
				   ([CompanyName]
				   ,[ContactFirstName]
				   ,[ContactLastName]
				   ,[Address]
				   ,[ZIP]
				   ,[City]
				   ,[Country]
				   ,[Phone]
				   ,[Email]
				   ,[CreatedOn]
				   ,[CreatedBy]
				   ,[IsActive]
				   ,[IsDeleted])
			 VALUES
			 (
				   @CompanyName
				   ,@ContactFirstName
				   ,@ContactLastName
				   ,@Address
				   ,@ZIP
				   ,@City
				   ,@Country
				   ,@Phone
				   ,@Email
				   ,GETDATE()
				   ,@CreatedBy
				   ,1
				   ,0
			)
	      
			SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=1
		END
		ELSE
		BEGIN
			SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=-2
		END
	END TRY
	BEGIN CATCH
		SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=-1
	END CATCH
		

