namespace ContactWeb.Migrations.ContactWebDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateElmahErrorTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'ELMAH_Error'))
BEGIN
    DROP TABLE dbo.ELMAH_Error
END

CREATE TABLE[dbo].[ELMAH_Error]  
(  
    [ErrorId][uniqueidentifier] NOT NULL,  
    [Application][nvarchar](60) NOT NULL,  
    [Host][nvarchar](50) NOT NULL,  
    [Type][nvarchar](100) NOT NULL,  
    [Source][nvarchar](60) NOT NULL,  
    [Message][nvarchar](500) NOT NULL,  
    [User][nvarchar](50) NOT NULL,  
    [StatusCode][int] NOT NULL,  
    [TimeUtc][datetime] NOT NULL,  
    [Sequence][int] IDENTITY(1, 1) NOT NULL,  
    [AllXml][ntext] NOT NULL  
)  
");
        }
        
        public override void Down()
        {
            Sql(@"IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'ELMAH_Error'))
                BEGIN
                    DROP TABLE dbo.ELMAH_Error
                END 
                ");
        }
    }
}
