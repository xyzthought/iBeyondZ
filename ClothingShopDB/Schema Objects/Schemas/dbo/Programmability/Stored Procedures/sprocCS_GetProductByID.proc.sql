CREATE PROCEDURE [dbo].[sprocCS_GetProductByID]
@ProductID INT 
AS
IF @ProductID = -1
BEGIN
	SET @ProductID = NULL
END
SELECT [ProductID]
      ,[ProductName]
      ,[Description]
      ,tb.[ManufacturerID]
      ,ISNULL(tm.CompanyName,'') AS Manufacturer
      ,tb.[CategoryID]
      ,ISNULL(CategoryName,'') AS CategoryName
      ,tb.[SizeID]
      ,ISNULL(SizeName,'') AS SizeName
      ,[BuyingPrice]
      ,[Tax]
      ,[Margin]
      ,[BarCode]
      ,tb.[CreatedOn]
      ,tb.[UpdatedOn]
      ,tb.[CreatedBy]
      ,tb.[UpdatedBy]
      ,tb.[IsActive]
      ,tb.[IsDeleted]
  FROM [dbo].[tblCS_Product] tb
  INNER JOIN tblCS_Manufacturer tm ON tm.ManufacturerID = tb.ManufacturerID
  INNER JOIN tblCS_Master_Category tc ON tc.CategoryID = tb.CategoryID
  INNER JOIN tblCS_Master_Size ts ON ts.SizeID = tb.SizeID
  WHERE ProductID = ISNULL(@ProductID,ProductID)
  AND tb.IsActive = 1 AND tb.IsDeleted = 0		