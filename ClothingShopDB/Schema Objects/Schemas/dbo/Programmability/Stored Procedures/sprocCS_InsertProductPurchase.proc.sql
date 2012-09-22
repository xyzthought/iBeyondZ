/*-- =============================================
-- Created by: Biswajit
-- Created on: 15-Sep-2012
-- Purpose: Insert Product Purchase details
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
exec sprocCS_InsertProductPurchase
-- =============================================*/
CREATE PROCEDURE [dbo].[sprocCS_InsertProductPurchase]
	@ManufacturerID INT
	,@PurchaseDate DATETIME
	,@ProductID INT
	,@SizeID INT
	,@Quantity DECIMAL(18,2)
	,@Price MONEY
	,@MessageID INT OUT
	,@Message NVARCHAR(255) OUT
AS
BEGIN
	BEGIN TRAN tranInsertProductPurchase
	DECLARE @PurchaseID INT
	
	BEGIN TRY
		INSERT INTO dbo.tblCS_Purchase
			(ManufacturerID, PurchaseDate)
		VALUES
			(@ManufacturerID, @PurchaseDate)
		
		SELECT @PurchaseID = SCOPE_IDENTITY() FROM dbo.tblCS_Purchase
		
		INSERT INTO dbo.tblCS_PurchaseDetail
			(PurchaseID, SizeID, Quantity, Price)
		VALUES
			(@PurchaseID, @SizeID, @Quantity, @Price)
		
		SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=1
			
		COMMIT TRAN tranInsertProductPurchase
	END TRY
	BEGIN CATCH
		SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=-1
		
		ROLLBACK TRAN tranInsertProductPurchase
	END CATCH
	
END