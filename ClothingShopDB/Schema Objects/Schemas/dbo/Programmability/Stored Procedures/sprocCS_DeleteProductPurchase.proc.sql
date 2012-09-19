/*-- =============================================
-- Created by: Biswajit
-- Created on: 25-Aug-2012
-- Purpose: Delete (logical delete) the Product Purchase Data
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
DECLARE @MessageID INT,
		@Message NVARCHAR(255)
exec sprocCS_DeleteProductPurchase 1,@MessageID OUT, @Message OUT
PRINT @MessageID
PRINT @Message
-- =============================================*/
CREATE PROCEDURE [dbo].[sprocCS_DeleteProductPurchase]
	@PurchaseID INT
	,@MessageID INT OUT
	,@Message NVARCHAR(255) OUT
AS

	BEGIN TRY
		UPDATE
			dbo.tblCS_Purchase
		SET
			IsDeleted = 0
		WHERE
			PurchaseID = @PurchaseID
			
		SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=3
		
	END TRY
	BEGIN CATCH
		SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=-4
	END CATCH
