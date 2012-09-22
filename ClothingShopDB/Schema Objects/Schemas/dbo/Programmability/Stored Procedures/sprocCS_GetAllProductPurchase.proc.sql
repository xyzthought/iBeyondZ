/*-- =============================================
-- Created by: Biswajit
-- Created on: 09-Sep-2012
-- Purpose: Returns All Product Purchase details from Manufacturer
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
exec sprocCS_GetAllProductPurchase null, null, 'CompanyName', 'ASC', 'ibeyondz'
-- =============================================*/
CREATE PROCEDURE [dbo].[sprocCS_GetAllProductPurchase]
	@PurchaseStartDate DATETIME = NULL,
	@PurchaseEndDate DATETIME = NULL,
	@SortColumnName	NVARCHAR(100) = NULL,     
	@SortDirection  NVARCHAR(100) = NULL,   
	@SearchText		NVARCHAR(100) = NULL 
AS
	IF @PurchaseStartDate IS NULL
	BEGIN
		SELECT @PurchaseStartDate= DATEADD(M,-1, GETDATE()), @PurchaseEndDate = GETDATE()
	END
		
	SELECT 
		P.PurchaseID
		,P.PurchaseDate
		,M.CompanyName
		,PR.ProductName
		,S.SizeName
		,PD.Quantity
		,PD.Price
		,(CASE 
				WHEN @SortDirection = 'ASC' THEN    
				CASE   
					WHEN @SortColumnName = 'PurchaseDate' THEN (ROW_NUMBER() OVER (ORDER BY P.PurchaseDate ASC))    
					WHEN @SortColumnName = 'CompanyName' THEN (ROW_NUMBER() OVER (ORDER BY M.CompanyName ASC))         
					WHEN @SortColumnName = 'ProductName' THEN (ROW_NUMBER() OVER (ORDER BY PR.ProductName ASC))
					WHEN @SortColumnName = 'SizeName' THEN (ROW_NUMBER() OVER (ORDER BY S.SizeName ASC))
					WHEN @SortColumnName = 'Quantity' THEN (ROW_NUMBER() OVER (ORDER BY PD.Quantity ASC))
					WHEN @SortColumnName = 'Price' THEN (ROW_NUMBER() OVER (ORDER BY PD.Price ASC)) 
				END  
				WHEN @SortDirection = 'DESC' THEN
				CASE   
					WHEN @SortColumnName = 'PurchaseDate' THEN (ROW_NUMBER() OVER (ORDER BY P.PurchaseDate DESC))    
					WHEN @SortColumnName = 'CompanyName' THEN (ROW_NUMBER() OVER (ORDER BY M.CompanyName DESC))         
					WHEN @SortColumnName = 'ProductName' THEN (ROW_NUMBER() OVER (ORDER BY PR.ProductName DESC))
					WHEN @SortColumnName = 'SizeName' THEN (ROW_NUMBER() OVER (ORDER BY S.SizeName DESC))
					WHEN @SortColumnName = 'Quantity' THEN (ROW_NUMBER() OVER (ORDER BY PD.Quantity DESC))
					WHEN @SortColumnName = 'Price' THEN (ROW_NUMBER() OVER (ORDER BY PD.Price DESC)) 
				END
			END) AS RowNumber
	FROM
		dbo.tblCS_Purchase P
	INNER JOIN
		dbo.tblCS_PurchaseDetail PD
	ON
		P.PurchaseID = PD.PurchaseID
	AND
		CONVERT(DATETIME, CONVERT(VARCHAR(10), P.PurchaseDate, 101)) BETWEEN 
		CONVERT(DATETIME, CONVERT(VARCHAR(10),@PurchaseStartDate, 101))
		AND
		CONVERT(DATETIME, CONVERT(VARCHAR(10),@PurchaseEndDate, 101))
	AND
		P.IsDeleted = 0
	INNER JOIN
		dbo.tblCS_Product PR
	ON
		PD.ProductID = PR.ProductID
	INNER JOIN
		dbo.tblCS_Master_Size S
	ON
		PD.SizeID = S.SizeID
	INNER JOIN
		dbo.tblCS_Manufacturer M
	ON
		P.ManufacturerID = M.ManufacturerID
	WHERE
		((@SearchText IS NULL)  
		OR   
		(M.CompanyName Like '%' + @SearchText +'%')  
		OR   
		(M.ContactFirstName Like '%' + @SearchText +'%')
		OR   
		(M.ContactLastName Like '%' + @SearchText +'%'))
	