CREATE PROCEDURE dbo.sprocCS_GetSize
@SizeID INT = NULL
AS
SELECT 
SizeID
,SizeName
FROM tblCS_Master_Size
WHERE SizeID = ISNULL(@SizeID,SizeID)


