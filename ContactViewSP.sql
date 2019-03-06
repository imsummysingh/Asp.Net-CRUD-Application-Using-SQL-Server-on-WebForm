CREATE PROCEDURE ContactViewSP
	@ContactID int
AS
BEGIN
	SELECT * FROM dbo.Contact WHERE ContactID=@ContactID
END