CREATE PROCEDURE sprocCS_InsertUpdatePlatformUser
 
 @UserID INT
,@UserTypeID INT
,@FirstName NVARCHAR(150)
,@LastName NVARCHAR(150)
,@CommunicationEmailID NVARCHAR(150)
,@LoginID NVARCHAR(150)
,@LoginPassword NVARCHAR(150)
,@ReturnValue INT OUT
,@ReturnMessage NVARCHAR(255) OUT

AS

-- Add part
IF @UserID=0
BEGIN
IF NOT EXISTS (SELECT 1 FROM tblCS_AppUserLogin WHERE LoginID=@LoginID AND IsActive=1 AND IsDeleted=0)
	BEGIN
	BEGIN TRY
		INSERT INTO [tblCS_AppUserLogin]
           ([UserTypeID]
           ,[FirstName]
           ,[LastName]
           ,[LoginID]
           ,[LoginPassword]
           ,[CommunicationEmailID]
           ,[CreatedOn]
           )
     VALUES
           (@UserTypeID
           ,@FirstName
           ,@LastName
           ,@LoginID 
           ,@LoginPassword
           ,@CommunicationEmailID
           ,GETDATE()
           )
           
        SET @ReturnValue=SCOPE_IDENTITY()
		SET @ReturnMessage='Operation Successful'
           
	END TRY
	BEGIN CATCH
		SET @ReturnValue=-1
		SET @ReturnMessage='Operation Failed'
	END CATCH
	END
	ELSE
	BEGIN
		SET @ReturnValue=-1
		SET @ReturnMessage='LoginID in use.'
	END
END




-- Edit Part
IF @UserID>0
BEGIN
IF NOT EXISTS (SELECT 1 FROM tblCS_AppUserLogin WHERE LoginID=@LoginID AND UserID<>@UserID AND IsActive=1 AND IsDeleted=0)
	BEGIN
		BEGIN TRY
			UPDATE [tblCS_AppUserLogin]
			SET [UserTypeID] = @UserTypeID
			  ,[FirstName] = @FirstName
			  ,[LastName] = @LastName
			  ,[LoginID] = @LoginID
			  ,[LoginPassword] = @LoginPassword
			  ,[CommunicationEmailID] = @CommunicationEmailID
			  ,[UpdatedOn] = GETDATE()
			WHERE UserID=@UserID
			
			SET @ReturnValue=@UserID
			SET @ReturnMessage='Operation Successful'
			
		END TRY
		BEGIN CATCH
			SET @ReturnValue=-1
			SET @ReturnMessage='Operation Failed'
		END CATCH
	END

	ELSE
	BEGIN
		SET @ReturnValue=-1
		SET @ReturnMessage='LoginID in use.'
	END

END