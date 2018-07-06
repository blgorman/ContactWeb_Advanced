namespace ContactWeb.Migrations.ContactWebDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSprocElmahGetErrorXml : DbMigration
    {
        public override void Up()
        {
            Sql(@"
CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]  
	@Application NVARCHAR(60),
	@ErrorId UNIQUEIDENTIFIER
AS
BEGIN
	SET NOCOUNT ON

	SELECT [AllXml]
	FROM [ELMAH_Error]
	WHERE [ErrorId] = @ErrorId
		AND [Application] = @Application
END

GRANT EXECUTE ON ELMAH_GetErrorXml TO PUBLIC
");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ELMAH_GetErrorXml')
BEGIN
	DROP PROCEDURE dbo.ELMAH_GetErrorXml
END
GO");
        }
    }
}
