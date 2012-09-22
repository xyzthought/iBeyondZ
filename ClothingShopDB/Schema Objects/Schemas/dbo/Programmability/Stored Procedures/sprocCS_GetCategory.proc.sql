CREATE PROCEDURE [dbo].[sprocCS_GetCategory]
@CategoryID INT = NULL
AS
SELECT 
CategoryID
,CategoryName
FROM tblCS_Master_Category
WHERE IsActive = 1 and CategoryID=ISNULL(@CategoryID,CategoryID)