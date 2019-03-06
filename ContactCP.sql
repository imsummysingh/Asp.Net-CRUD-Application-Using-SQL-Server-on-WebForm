CREATE PROCEDURE [dbo].[ContactCP]
	@ContactID  int,
	@Name Nvarchar(50),
	@MobileNo nvarchar(50),
	@Address nvarchar(250)
AS
BEGIN

if(@ContactID=0)
	BEGIN
		INSERT INTO dbo.Contact ([Name],MobileNo,[Address]) Values (@Name,@MobileNo,@Address);
	END
ELSE
	BEGIN
		UPDATE dbo.Contact SET 
			[Name]=@Name,
			MobileNo=@MobileNo,
			[Address]=@Address
		WHERE ContactID=@ContactID
	END

END