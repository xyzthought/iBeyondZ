CREATE PROCEDURE [sprocCS_UpdateProduct]
	@ProductID INT,
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
UPDATE tblCS_Product
	SET 
	ProductName = @ProductName,
	Description = @Description,
	ManufacturerID = @ManufacturerID,
	CategoryID = @CategoryID,
	SizeID = @SizeID,
	BuyingPrice = @BuyingPrice,
	Tax = @Tax,
	Margin = @Margin,
	BarCode = @BarCode,
	CreatedBy = @CreatedBy,
	UpdatedBy = @UpdatedBy
WHERE ProductID = @ProductID
END