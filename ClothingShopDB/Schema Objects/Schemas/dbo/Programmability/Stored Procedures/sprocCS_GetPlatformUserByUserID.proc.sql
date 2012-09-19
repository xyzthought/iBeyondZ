/*
sprocCS_GetPlatformUserByUserID 3
*/
CREATE PROCEDURE sprocCS_GetPlatformUserByUserID
@UserID INT
AS
SELECT [UserID]
      ,[UserTypeID]
      ,[FirstName]
      ,[LastName]
      ,[LoginID]
      ,[LoginPassword]
      ,[CommunicationEmailID]
  FROM [tblCS_AppUserLogin]
  WHERE UserID=@UserID
