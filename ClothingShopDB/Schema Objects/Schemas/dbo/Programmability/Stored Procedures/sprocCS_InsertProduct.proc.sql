CREATE PROCEDURE [sprocCS_InsertProduct]
	@ProductName nvarchar(500),
	@Description nvarchar(4000),
	@ManufacturerID int,
	@CategoryID int,
	@SizeID int,
	@BuyingPrice decimal(18,2),
	@Tax decimal(18,2),
	@Margin decimal(18,2),
	@BarCode nvarchar(500),
	@CreatedBy int,
	@UpdatedBy int
AS
BEGIN
INSERT INTO tblCS_Product(
	ProductName,
	Description,
	ManufacturerID,
	CategoryID,
	SizeID,
	BuyingPrice,
	Tax,
	Margin,
	BarCode,
	CreatedBy,
	UpdatedBy
)
VALUES (
	@ProductName,
	@Description,
	@ManufacturerID,
	@CategoryID,
	@SizeID,
	@BuyingPrice,
	@Tax,
	@Margin,
	@BarCode,
	@CreatedBy,
	@UpdatedBy
)
END