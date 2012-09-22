/*-- =============================================
-- Created by: Biswajit
-- Created on: 15-Sep-2012
-- Purpose: Update Product Purchase details
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
exec sprocCS_UpdateProductPurchase
-- =============================================*/
CREATE PROCEDURE [dbo].[sprocCS_UpdateProductPurchase]
	@PurchaseID INT
	,@ManufacturerID INT
	,@PurchaseDate DATETIME
	,@ProductID INT
	,@SizeID INT
	,@Quantity DECIMAL(18,2)
	,@Price MONEY
	,@MessageID INT OUT
	,@Message NVARCHAR(255) OUT
AS
BEGIN
	BEGIN TRAN tranUpdateProductPurchase
	
	BEGIN TRY
		UPDATE dbo.tblCS_Purchase 
		SET 
			ManufacturerID = @ManufacturerID, PurchaseDate=@PurchaseDate
		WHERE
			 PurchaseID = @PurchaseID
		
		UPDATE dbo.tblCS_PurchaseDetail
		SET
			SizeID = @SizeID, Quantity=@Quantity, Price=@Price
		WHERE
			PurchaseID = @PurchaseID
				
		SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=1
			
		COMMIT TRAN tranUpdateProductPurchase
	END TRY
	BEGIN CATCH
		SELECT @MessageID = MessageID, @Message=StatusMessage 
			FROM dbo.tblCS_Master_Message WHERE MessageID=-5
		
		ROLLBACK TRAN tranUpdateProductPurchase
	END CATCH
	
END