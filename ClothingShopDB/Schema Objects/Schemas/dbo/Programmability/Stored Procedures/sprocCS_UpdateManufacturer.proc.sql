/*-- =============================================
-- Created by: Biswajit
-- Created on: 25-Aug-2012
-- Purpose: Update Manufacturer with the supplier value
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
DECLARE @MessageID INT,
		@Message NVARCHAR(255)
exec sprocCS_UpdateManufacturer 
1, 'iBeyondZ', 'Sudip', 'Chakraborty', 'Kasba', '700018', 'Kolkata',
'India', '1234567890', 'support@ibeyondz.com', 1, @MessageID OUT, @Message OUT
PRINT @MessageID
PRINT @Message
-- =============================================*/

CREATE PROCEDURE dbo.sprocCS_UpdateManufacturer
	@ManufacturerID INT
	,@CompanyName NVARCHAR(250)
	,@ContactFirstName NVARCHAR(150)
	,@ContactLastName NVARCHAR(150)
	,@Address NVARCHAR(1000)
	,@ZIP NVARCHAR(50)
	,@City NVARCHAR(150)
	,@Country NVARCHAR(150)
	,@Phone NVARCHAR(150)
	,@Email NVARCHAR(150)
	,@UpdatedBy INT
	,@MessageID INT OUT
	,@Message NVARCHAR(255) OUT
AS
	BEGIN TRY
	
		IF NOT EXISTS(SELECT ManufacturerID FROM dbo.tblCS_Manufacturer 
						WHERE CompanyName=@CompanyName AND IsDeleted = 0
						AND ManufacturerID <> @ManufacturerID)
		BEGIN
			UPDATE [dbo].[tblCS_Manufacturer]
			SET [CompanyName] = @CompanyName
			  ,[ContactFirstName] = @ContactFirstName
			  ,[ContactLastName] = @ContactLastName
			  ,[Address] = @Address
			  ,[ZIP] = @ZIP
			  ,[City] = @City
			  ,[Country] = @Country
			  ,[Phone] = @Phone
			  ,[Email] = @Email
			  ,[UpdatedOn] = GETDATE()
			  ,[UpdatedBy] = @UpdatedBy
			WHERE
				ManufacturerID = @ManufacturerID
			
			SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=2
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
