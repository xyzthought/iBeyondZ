  
/*  
[sprocCS_DeletePlatformUser] 
*/  
CREATE PROCEDURE [dbo].[sprocCS_DeletePlatformUser]  
 @UserID INT
,@ReturnValue INT OUT
,@ReturnMessage NVARCHAR(255) OUT
 
AS  
  
BEGIN TRY
UPDATE
tblCS_AppUserLogin
SET 
 IsActive=0
,IsDeleted=1
WHERE UserID=@UserID

SET @ReturnValue=@UserID
SET @ReturnMessage='Operation Successful'

END TRY
BEGIN CATCH
SET @ReturnValue=-1
SET @ReturnMessage='Error in Operation'
END CATCH		