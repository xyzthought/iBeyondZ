/*-- =============================================
-- Created by: Biswajit
-- Created on: 25-Aug-2012
-- Purpose: Returns All Manufacturer
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
exec sprocCS_GetAllManufacturer 'CompanyName', 'ASC', 'ibeyondz'
-- =============================================*/

CREATE PROCEDURE [dbo].[sprocCS_GetAllManufacturer]
	@SortColumnName	NVARCHAR(100) = NULL,     
	@SortDirection  NVARCHAR(100) = NULL,   
	@SearchText		NVARCHAR(100) = NULL 
AS
	SELECT 
		M.[ManufacturerID]
		,M.[CompanyName]
		,M.[ContactFirstName]
		,M.[ContactLastName]
		,M.[Address]
		,M.[ZIP]
		,M.[City]
		,M.[Country]
		,M.[Phone]
		,M.[Email]
		,M.[IsActive]
		,(CASE 
			WHEN @SortDirection = 'ASC' THEN    
			CASE   
				WHEN @SortColumnName = 'CompanyName' THEN (ROW_NUMBER() OVER (ORDER BY M.CompanyName ASC))    
				WHEN @SortColumnName = 'ContactFirstName' THEN (ROW_NUMBER() OVER (ORDER BY M.ContactFirstName ASC))         
				WHEN @SortColumnName = 'ContactLastName' THEN (ROW_NUMBER() OVER (ORDER BY M.ContactLastName ASC))    
				WHEN @SortColumnName = 'Email' THEN (ROW_NUMBER() OVER (ORDER BY M.Email ASC))   
			END  
			WHEN @SortDirection = 'DESC' THEN
			CASE   
				WHEN @SortColumnName = 'CompanyName' THEN (ROW_NUMBER() OVER (ORDER BY M.CompanyName DESC))    
				WHEN @SortColumnName = 'ContactFirstName' THEN (ROW_NUMBER() OVER (ORDER BY M.ContactFirstName DESC))         
				WHEN @SortColumnName = 'ContactLastName' THEN (ROW_NUMBER() OVER (ORDER BY M.ContactLastName DESC))    
				WHEN @SortColumnName = 'Email' THEN (ROW_NUMBER() OVER (ORDER BY M.Email DESC))   
			END
		END) AS RowNumber
	FROM 
		[dbo].[tblCS_Manufacturer] M
	WHERE
		((@SearchText IS NULL)  
		OR   
		(M.CompanyName Like '%' + @SearchText +'%')  
		OR   
		(M.ContactFirstName Like '%' + @SearchText +'%')
		OR   
		(M.ContactLastName Like '%' + @SearchText +'%'))
		AND
		IsDeleted = 0
	ORDER BY 
		RowNumber
