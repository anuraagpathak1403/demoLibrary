namespace demoLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDbModal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.assetLibraries",
                c => new
                    {
                        bookId = c.Int(nullable: false, identity: true),
                        bookName = c.String(),
                        category = c.String(),
                        authorName = c.String(),
                        columnWise = c.String(),
                        bookBarcodeId = c.Binary(),
                    })
                .PrimaryKey(t => t.bookId);
            
            CreateTable(
                "dbo.assetLibraryManagements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        enrollmentId = c.String(),
                        barcodeId = c.Int(nullable: false),
                        assetId = c.Int(nullable: false),
                        issueDate = c.DateTime(nullable: false),
                        returnDate = c.DateTime(nullable: false),
                        lateFine = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.assetLibraries", t => t.assetId, cascadeDelete: true)
                .Index(t => t.assetId);
            
            CreateTable(
                "dbo.registrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        enrollmentId = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        fatherName = c.String(),
                        motherName = c.String(),
                        password = c.Binary(),
                        passwordSalt = c.Binary(),
                        DateOfBirth = c.DateTime(nullable: false),
                        created = c.DateTime(nullable: false),
                        gender = c.String(),
                        city = c.String(),
                        country = c.String(),
                        boolLibrary = c.Boolean(nullable: false),
                        qrCode = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.assetLibraryManagements", "assetId", "dbo.assetLibraries");
            DropIndex("dbo.assetLibraryManagements", new[] { "assetId" });
            DropTable("dbo.registrations");
            DropTable("dbo.assetLibraryManagements");
            DropTable("dbo.assetLibraries");
        }
    }
}
