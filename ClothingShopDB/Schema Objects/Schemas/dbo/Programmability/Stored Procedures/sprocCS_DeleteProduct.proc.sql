CREATE PROC dbo.sprocCS_DeleteProduct
@ProductID INT
AS

UPDATE tblCS_Product
SET IsDeleted = 1
WHERE ProductID = @ProductID