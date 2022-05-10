CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id NVARCHAR(128),
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@EmailAddress nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[User] ([Id], [FirstName], [LastName], [EmailAddress])
	VALUES (@Id, @FirstName, @LastName, @EmailAddress)
END