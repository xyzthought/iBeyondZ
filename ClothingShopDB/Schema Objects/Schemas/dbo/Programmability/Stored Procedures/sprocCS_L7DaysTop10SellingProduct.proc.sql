CREATE PROCEDURE dbo.[sprocCS_L7DaysTop10SellingProduct]

 @SortColumnName  NVARCHAR(100) = NULL,     
 @SortDirection  NVARCHAR(100) = NULL,   
 @SearchText   NVARCHAR(100) = NULL 
 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 10
	SD.ProductID,
	POD.ProductName,
	SZ.SizeName,
	SUM(SD.Quantity) AS Quantity, 
	SUM(SD.Price) AS Price 
	,(CASE   
       WHEN @SortDirection = 'ASC' THEN    
	   CASE   
		 WHEN @SortColumnName = 'ProductName' THEN (ROW_NUMBER() OVER (ORDER BY POD.ProductName ASC))    
		 WHEN @SortColumnName = 'SizeName' THEN (ROW_NUMBER() OVER (ORDER BY SZ.SizeName ASC))           
	   END  
			WHEN @SortDirection = 'DESC' THEN    
	   CASE   
		 WHEN @SortColumnName = 'ProductName' THEN (ROW_NUMBER() OVER (ORDER BY POD.ProductName DESC))    
		 WHEN @SortColumnName = 'SizeName' THEN (ROW_NUMBER() OVER (ORDER BY SZ.SizeName DESC))     
	   END  
   END) AS RowNumber
	FROM tblCS_SaleDetails SD
	INNER JOIN dbo.tblCS_Product POD
	ON SD.ProductID=POD.ProductID
	INNER JOIN dbo.tblCS_Master_Size SZ
	ON SD.SizeID=SZ.SizeID
	AND 
	SD.SaleID IN (SELECT SaleID FROM dbo.tblCS_Sale WHERE SaleDate>=dateadd(day,datediff(day,0,GetDate())- 7,0))
	
	GROUP BY SD.ProductID,POD.ProductName,SZ.SizeName
	ORDER BY SUM(SD.Quantity) DESC,RowNumber
	END