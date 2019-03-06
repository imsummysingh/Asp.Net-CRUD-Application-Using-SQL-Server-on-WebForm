CREATE PROCEDURE [dbo].[DeleteSP]
	@ContactID int
AS
BEGIN
	DELETE FROM dbo.Contact WHERE ContactID=@ContactID 
END