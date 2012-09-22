/*
[sprocCS_GetAllAppUser]
*/ 
CREATE PROCEDURE [dbo].[sprocCS_GetAllAppUser]  
   
 @SortColumnName  NVARCHAR(100) = NULL,     
 @SortDirection  NVARCHAR(100) = NULL,   
 @SearchText   NVARCHAR(100) = NULL  
    
AS
BEGIN
	SET NOCOUNT ON;
    
	SELECT	   AUL.[UserID]
			  ,AUL.[UserTypeID]
			  ,UT.[UserType]
			  ,AUL.[FirstName]
			  ,AUL.[LastName]
			  ,AUL.[CommunicationEmailID]
			  ,ISNULL(AUL.[LastLoggedIn],GETDATE()) AS [LastLoggedIn]
			  ,AUL.[CreatedOn]
			  ,AUL.[UpdatedOn]
			  ,AUL.[IsActive]
			  ,AUL.IsDeleted		
  ,(CASE   
       WHEN @SortDirection = 'ASC' THEN    
	   CASE   
		 WHEN @SortColumnName = 'FirstName' THEN (ROW_NUMBER() OVER (ORDER BY AUL.FirstName ASC))    
		 WHEN @SortColumnName = 'LastName' THEN (ROW_NUMBER() OVER (ORDER BY AUL.LastName ASC))         
		 WHEN @SortColumnName = 'CreatedOn' THEN (ROW_NUMBER() OVER (ORDER BY AUL.CreatedOn ASC))    
		 WHEN @SortColumnName = 'UpdatedOn' THEN (ROW_NUMBER() OVER (ORDER BY AUL.UpdatedOn ASC))   
	   END  
			WHEN @SortDirection = 'DESC' THEN    
	   CASE   
		 WHEN @SortColumnName = 'FirstName' THEN (ROW_NUMBER() OVER (ORDER BY AUL.FirstName DESC))    
		 WHEN @SortColumnName = 'LastName' THEN (ROW_NUMBER() OVER (ORDER BY AUL.LastName DESC))   
		 WHEN @SortColumnName = 'CreatedOn' THEN (ROW_NUMBER() OVER (ORDER BY AUL.CreatedOn DESC))   
		 WHEN @SortColumnName = 'UpdatedOn' THEN (ROW_NUMBER() OVER (ORDER BY AUL.UpdatedOn DESC))    
	   END  
   END) AS RowNumber
   
  FROM tblCS_AppUserLogin AUL
  INNER JOIN dbo.tblCS_Master_UserType UT
  ON UT.UsertypeID=AUL.UserTypeID
  
  
	WHERE ((@SearchText IS NULL)  
			 OR   
			  (AUL.FirstName Like '%' + @SearchText +'%')  
			 OR   
			  (AUL.LastName Like '%' + @SearchText +'%'))
	
	 AND AUL.IsActive=1 
     AND AUL.IsDeleted=0
  
	ORDER BY 
		RowNumber
END
