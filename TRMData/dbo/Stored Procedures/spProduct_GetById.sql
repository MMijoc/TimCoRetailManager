CREATE PROCEDURE [dbo].[spProduct_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [ProductName], [Description], [RetailPrice], [QuantityInStock], [IsTaxable], [ProductImage]
	FROM [dbo].[Product]
	WHERE Id = @Id
END