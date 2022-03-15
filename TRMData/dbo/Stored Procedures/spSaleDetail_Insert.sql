CREATE PROCEDURE [dbo].[spSaleDetail_Insert]
	@SaleId INT OUTPUT,
	@ProductId INT,
	@Quantity INT,
	@PurchasePrice MONEY,
	@Tax MONEY

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[SaleDetail](SaleId, ProductId, Quantity, PurchasePrice, Tax)
	VALUES (@SaleId, @ProductId, @Quantity, @PurchasePrice, @Tax);
END