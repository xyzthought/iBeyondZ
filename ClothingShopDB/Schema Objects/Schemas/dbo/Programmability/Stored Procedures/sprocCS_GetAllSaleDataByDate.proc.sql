CREATE PROCEDURE dbo.[sprocCS_GetAllSaleDataByDate]

@SortColumnName  NVARCHAR(100) = NULL,     
@SortDirection  NVARCHAR(100) = NULL,   
@SearchText   NVARCHAR(100) = NULL,
@SaleDate VARCHAR(10)=NULL

AS 
BEGIN

IF @SaleDate IS NULL
SET @SaleDate=CONVERT(VARCHAR(10),GetDate(),101)

	SET NOCOUNT ON;
	SELECT 
	S.SaleID
	,'SO-'+CONVERT(VARCHAR(30),SaleID) AS SaleOrder
	,C.FirstName
	,C.LastName
	,S.SaleDate
	,P.PaymentMode
	,(SELECT SUM(Price) FROM dbo.tblCS_SaleDetails WHERE SaleID=S.SaleID) AS Price
	,S.StandardRebate
	,S.Discount
	,A.FirstName+' '+A.LastName AS SaleMadeBy
	,(CASE   
		   WHEN @SortDirection = 'ASC' THEN    
		   CASE   
			 WHEN @SortColumnName = 'FirstName' THEN (ROW_NUMBER() OVER (ORDER BY C.FirstName ASC))    
			 WHEN @SortColumnName = 'SaleMadeBy' THEN (ROW_NUMBER() OVER (ORDER BY A.FirstName ASC))           
		   END  
				WHEN @SortDirection = 'DESC' THEN    
		   CASE   
			 WHEN @SortColumnName = 'FirstName' THEN (ROW_NUMBER() OVER (ORDER BY C.FirstName DESC))    
			 WHEN @SortColumnName = 'SaleMadeBy' THEN (ROW_NUMBER() OVER (ORDER BY A.FirstName DESC))     
		   END  
	   END) AS RowNumber
	FROM 

	dbo.tblCS_Sale S
	INNER JOIN tblCS_Customer C
	ON S.CustomerID=C.CustomerID
	INNER JOIN dbo.tblCS_Master_PaymentMode P
	on P.PaymentModeID=S.PaymentModeID
	INNER JOIN dbo.tblCS_AppUserLogin A
	ON A.UserID=S.SaleMadeBy
	
	
	WHERE ((@SearchText IS NULL)  
			 OR   
			  (A.FirstName Like '%' + @SearchText +'%')  
			 OR   
			  (C.FirstName Like '%' + @SearchText +'%'))
	AND S.SaleDate=@SaleDate		  
	ORDER BY RowNumber
END