  
/*  
sprocCSWeb_AuthenticationValidation 'sbanik','123'  
*/  
CREATE PROCEDURE sprocCS_AuthenticationValidation  
@LoginID NVARCHAR(150),  
@LoginPassword NVARCHAR(150)  
AS  
  
IF EXISTS(SELECT 1 FROM dbo.tblCS_AppUserLogin WHERE LoginID=@LoginID AND LoginPassword=@LoginPassword AND IsActive=1 AND IsDeleted=0) 
BEGIN 
SELECT   
    AUL.[UserID]  
      ,AUL.[UserTypeID]  
      ,UT.[UserType]  
      ,AUL.[FirstName]  
      ,AUL.[LastName]  
      ,AUL.[LoginID]  
      ,AUL.[LoginPassword]  
      ,AUL.[CommunicationEmailID]  
      ,AUL.[LastLoggedIn]  
      ,AUL.[CreatedOn]  
      ,AUL.[UpdatedOn]  
      ,AUL.[IsActive]  
      ,AUL.[IsDeleted]  
  FROM tblCS_AppUserLogin AUL  
  INNER JOIN dbo.tblCS_Master_UserType UT  
  ON UT.UsertypeID=AUL.UserTypeID  
  WHERE LoginID=@LoginID AND LoginPassword=@LoginPassword AND IsActive=1 AND IsDeleted=0  
  
  UPDATE tblCS_AppUserLogin SET [LastLoggedIn]=GETDATE() WHERE LoginID=@LoginID AND LoginPassword=@LoginPassword AND IsActive=1 AND IsDeleted=0 
  
  END