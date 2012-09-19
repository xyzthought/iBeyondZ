/*-- =============================================
-- Created by: Biswajit
-- Created on: 25-Aug-2012
-- Purpose: Returns Product Purchase details as per Supplied ID
-- Modifications:
-- <Date> <Programmer> <Change>
------------------------------------------------
exec sprocCS_GetProductPurchaseByID 1
-- =============================================*/
CREATE PROCEDURE [dbo].[sprocCS_GetProductPurchaseByID]
	@PurchaseID INT
AS
	SELECT 
		P.PurchaseID
		,P.PurchaseDate
		,M.ManufacturerID
		,M.CompanyName
		,PR.ProductID
		,PR.ProductName
		,S.SizeID
		,S.SizeName
		,PD.Quantity
		,PD.Price
	FROM
		dbo.tblCS_Purchase P
	INNER JOIN
		dbo.tblCS_PurchaseDetail PD
	ON
		P.PurchaseID = PD.PurchaseID
	AND
		P.IsDeleted = 0
	AND
		P.PurchaseID = @PurchaseID
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