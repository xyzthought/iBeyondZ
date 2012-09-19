CREATE PROCEDURE [dbo].[sprocCS_GetProducts]
@ProductID INT = NULL,
@SortColumnName	NVARCHAR(100) = NULL,     
@SortDirection  NVARCHAR(100) = NULL,   
@SearchText		NVARCHAR(100) = NULL 
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
      ,(CASE 
			WHEN @SortDirection = 'ASC' THEN    
			CASE   
				WHEN @SortColumnName = 'ProductName' THEN (ROW_NUMBER() OVER (ORDER BY tb.ProductName ASC))    
				WHEN @SortColumnName = 'Manufacturer' THEN (ROW_NUMBER() OVER (ORDER BY tm.CompanyName ASC))         
				WHEN @SortColumnName = 'CategoryName' THEN (ROW_NUMBER() OVER (ORDER BY tc.CategoryName ASC))    
				  
			END  
			WHEN @SortDirection = 'DESC' THEN
			CASE   
				WHEN @SortColumnName = 'ProductName' THEN (ROW_NUMBER() OVER (ORDER BY tb.ProductName DESC))    
				WHEN @SortColumnName = 'Manufacturer' THEN (ROW_NUMBER() OVER (ORDER BY tm.CompanyName  DESC))         
				WHEN @SortColumnName = 'CategoryName' THEN (ROW_NUMBER() OVER (ORDER BY tc.CategoryName DESC))    
				
			END
		END) AS RowNumber
  FROM [dbo].[tblCS_Product] tb
  INNER JOIN tblCS_Manufacturer tm ON tm.ManufacturerID = tb.ManufacturerID
  INNER JOIN tblCS_Master_Category tc ON tc.CategoryID = tb.CategoryID
  INNER JOIN tblCS_Master_Size ts ON ts.SizeID = tb.SizeID
  WHERE ProductID = ISNULL(@ProductID,ProductID)
  AND tb.IsActive = 1 AND tb.IsDeleted = 0 AND
  	((@SearchText IS NULL)  
		OR   
		(tb.ProductName Like '%' + @SearchText +'%')  
		OR   
		(tm.CompanyName Like '%' + @SearchText +'%')
		OR   
		(tc.CategoryName Like '%' + @SearchText +'%'))
		
ORDER BY RowNumber		
		