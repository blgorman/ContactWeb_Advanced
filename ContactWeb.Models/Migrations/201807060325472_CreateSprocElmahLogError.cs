namespace ContactWeb.Migrations.ContactWebDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSprocElmahLogError : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE PROCEDURE [dbo].[ELMAH_LogError] 
    @ErrorId UNIQUEIDENTIFIER,    
    @Application NVARCHAR(60),    
    @Host NVARCHAR(30),    
    @Type NVARCHAR(100),  
    @Source NVARCHAR(60),    
    @Message NVARCHAR(500),  
    @User NVARCHAR(50),   
    @AllXml NTEXT,    
    @StatusCode INT,   
    @TimeUtc DATETIME
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [ELMAH_Error]
	(  
		[ErrorId],   
		[Application],   
		[Host],  
		[Type],  
		[Source],  
		[Message],    
		[User],    
		[AllXml],    
		[StatusCode],    
		[TimeUtc]  
	)  
	VALUES 
    (  
		@ErrorId,  
		@Application,    
		@Host,    
		@Type,    
		@Source,   
		@Message,    
		@User,   
		@AllXml,   
		@StatusCode,   
		@TimeUtc 
	)  
END

GRANT EXECUTE ON [dbo].[ELMAH_LogError] TO PUBLIC
");
        }
        
        public override void Down()
        {
            Sql(@"IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ELMAH_LogError')
BEGIN
	DROP PROCEDURE [dbo].[ELMAH_LogError]
END
GO");
        }
    }
}
