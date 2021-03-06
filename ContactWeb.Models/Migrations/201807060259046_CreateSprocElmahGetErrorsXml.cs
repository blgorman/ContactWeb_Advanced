namespace ContactWeb.Migrations.ContactWebDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSprocElmahGetErrorsXml : DbMigration
    {
        public override void Up()
        {
            Sql(@"
CREATE PROCEDURE dbo.ELMAH_GetErrorsXml
    @Application NVARCHAR(60),  
    @PageIndex INT = 0,  
    @PageSize INT = 15,  
    @TotalCount INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @FirstTimeUTC DATETIME  
	DECLARE @FirstSequence INT  
	DECLARE @StartRow INT  
	DECLARE @StartRowIndex INT 
 
	SELECT @TotalCount = COUNT(1)  
	FROM [ELMAH_Error]  
	WHERE [Application] = @Application  

	SET @StartRowIndex = @PageIndex * @PageSize + 1  

	IF @StartRowIndex <= @TotalCount  
	BEGIN  
		SET ROWCOUNT @StartRowIndex  
  
		SELECT  
			@FirstTimeUTC = [TimeUtc],  
			@FirstSequence = [Sequence]  
		FROM [ELMAH_Error]  
		WHERE [Application] = @Application  
		ORDER BY [TimeUtc] DESC, [Sequence] DESC  
	END  
	ELSE  
	BEGIN  
		SET @PageSize = 0  
	END  

	SET ROWCOUNT @PageSize  
  
	SELECT 
		errorId = [ErrorId],  
		application = [Application],  
		host = [Host],  
		type = [Type],  
		source = [Source],  
		message = [Message],  
		[user] = [User],  
		statusCode = [StatusCode],  
		time = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'  
	FROM [ELMAH_Error] error  
	WHERE [Application] = @Application  
		AND [TimeUtc] <= @FirstTimeUTC  
		AND [Sequence] <= @FirstSequence  
	ORDER BY [TimeUtc] DESC, [Sequence] DESC  
	FOR XML AUTO  
END

GRANT EXECUTE ON dbo.ELMAH_GetErrorsXml TO PUBLIC
");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ELMAH_GetErrorsXml')
BEGIN
	DROP PROCEDURE dbo.ELMAH_GetErrorsXml
END");
        }
    }
}
