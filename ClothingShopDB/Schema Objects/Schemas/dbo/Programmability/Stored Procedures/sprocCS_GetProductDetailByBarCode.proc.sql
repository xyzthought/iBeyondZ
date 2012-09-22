/*
sprocCS_GetProductDetailByBarCode '445212121' 
Select * from dbo.tblCS_Product 
*/

CREATE PROCEDURE sprocCS_GetProductDetailByBarCode
@ProductBarcode NVARCHAR(500)

AS

BEGIN
	IF EXISTS(SELECT 1 FROM dbo.tblCS_Product WHERE BarCode=@ProductBarcode and IsActive=1 AND IsDeleted=0)
	BEGIN
		SELECT 
		 P.ProductID
		,p.BarCode
		,P.ProductName
		,P.SizeID
		,S.SizeName
		,P.BuyingPrice+(P.BuyingPrice*P.Tax/100)+P.Margin AS Price
		FROM 
		dbo.tblCS_Product P 
		INNER JOIN dbo.tblCS_Master_Size S
		ON S.SizeID=P.SizeID
		WHERE P.BarCode=@ProductBarcode and P.IsActive=1 AND P.IsDeleted=0
	END
END	