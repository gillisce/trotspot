namespace HorseShow_Framework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Division",
                c => new
                    {
                        intDivisionID = c.Int(nullable: false, identity: true),
                        strDivisionName = c.String(),
                        strDivisionDescr = c.String(),
                        intMaxAge = c.Int(),
                        intMinAge = c.Int(),
                        intMaxNumRiders = c.Int(),
                        intMinNumRiders = c.Int(),
                        dtmCreated = c.DateTime(),
                        strCreatorID = c.String(),
                        dtmModified = c.DateTime(),
                        strModifierID = c.String(),
                    })
                .PrimaryKey(t => t.intDivisionID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Division");
        }
    }
}
