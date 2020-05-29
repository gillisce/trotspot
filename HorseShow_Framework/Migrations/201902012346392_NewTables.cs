namespace HorseShow_Framework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        intClassID = c.Int(nullable: false, identity: true),
                        intClassNumber = c.Int(nullable: false),
                        strClassName = c.String(),
                        fltCost = c.Single(),
                        intMaxNumRiders = c.Int(),
                        intMinNumRiders = c.Int(),
                        ysnActive = c.Boolean(nullable: false),
                        dtmCreated = c.DateTime(),
                        strCreatorID = c.String(),
                        dtmModified = c.DateTime(),
                        strModifierID = c.String(),
                    })
                .PrimaryKey(t => t.intClassID);
            
            CreateTable(
                "dbo.ClassPlacing",
                c => new
                    {
                        intClassPlacing = c.Int(nullable: false, identity: true),
                        intHorseRiderID = c.Int(nullable: false),
                        intClassID = c.Int(nullable: false),
                        intPlace = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.intClassPlacing);
            
            CreateTable(
                "dbo.ClassRegistration",
                c => new
                    {
                        intClassRegistrationID = c.Int(nullable: false, identity: true),
                        intHorseRiderID = c.Int(nullable: false),
                        intClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.intClassRegistrationID);
            
            CreateTable(
                "dbo.DivisionClassMapping",
                c => new
                    {
                        intDivClassID = c.Int(nullable: false, identity: true),
                        intDivisionID = c.Int(nullable: false),
                        strDivisionName = c.String(),
                        intClassID = c.Int(nullable: false),
                        strClassName = c.String(),
                        ysnActive = c.Boolean(nullable: false),
                        dtmCreated = c.DateTime(),
                        strCreatorID = c.String(),
                        dtmModified = c.DateTime(),
                        strModifierID = c.String(),
                    })
                .PrimaryKey(t => t.intDivClassID);
            
            CreateTable(
                "dbo.GlobalShowSettings",
                c => new
                    {
                        intGlobalShowSettingsID = c.Int(nullable: false, identity: true),
                        strParamName = c.String(),
                        strParamDescr = c.String(),
                        intParameterValue = c.Int(),
                        intYsNActive = c.Boolean(nullable: false),
                        dtmCreated = c.DateTime(),
                        strCreatorID = c.String(),
                        dtmModified = c.DateTime(),
                        strModifierID = c.String(),
                    })
                .PrimaryKey(t => t.intGlobalShowSettingsID);
            
            CreateTable(
                "dbo.HorseRider",
                c => new
                    {
                        intHorseRiderID = c.Int(nullable: false, identity: true),
                        intPersonID = c.Int(nullable: false),
                        strHorseName = c.String(nullable: false),
                        intRiderNumber = c.Int(nullable: false),
                        ysnCheckedIn = c.Boolean(nullable: false),
                        fltAmountDue = c.Single(nullable: false),
                        ysnHasPaid = c.Boolean(nullable: false),
                        strPaymentMethod = c.String(),
                        intDivisionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.intHorseRiderID);
            
            CreateTable(
                "dbo.HorseRiderScores",
                c => new
                    {
                        intHorseRiderScores = c.Int(nullable: false, identity: true),
                        intHorseRiderID = c.Int(nullable: false),
                        intOverallScore = c.Int(nullable: false),
                        intNum1stPlace = c.Int(),
                        intNum2ndPlace = c.Int(),
                        intNum3rdPlace = c.Int(),
                        intNum4thPlace = c.Int(),
                        intNum5thPlace = c.Int(),
                    })
                .PrimaryKey(t => t.intHorseRiderScores);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        intPersonID = c.Int(nullable: false, identity: true),
                        strFirstName = c.String(nullable: false),
                        strLastName = c.String(nullable: false),
                        intAge = c.Int(),
                        intPhoneNumber = c.Int(nullable: false),
                        strEmail = c.String(nullable: false),
                        strAddress = c.String(),
                        strCity = c.String(),
                        strState = c.String(),
                        intZipCode = c.Int(nullable: false),
                        dtmCreated = c.DateTime(),
                        strModifierID = c.String(),
                    })
                .PrimaryKey(t => t.intPersonID);
            
            CreateTable(
                "dbo.PlacePointValues",
                c => new
                    {
                        intPlacePointValues = c.Int(nullable: false, identity: true),
                        intPlace = c.Int(nullable: false),
                        intNumPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.intPlacePointValues);
            
            CreateTable(
                "dbo.RiderAddOns",
                c => new
                    {
                        intRiderAddOnsID = c.Int(nullable: false, identity: true),
                        intStoreItemID = c.Int(nullable: false),
                        intHorseRiderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.intRiderAddOnsID);
            
            CreateTable(
                "dbo.StoreItems",
                c => new
                    {
                        intItemID = c.Int(nullable: false, identity: true),
                        strItemName = c.String(),
                        strItemDescription = c.String(),
                        fltCost = c.Single(),
                        yesnActive = c.Boolean(nullable: false),
                        dtmCreated = c.DateTime(),
                        strCreatorID = c.String(),
                        dtmModified = c.DateTime(),
                        strModifierID = c.String(),
                    })
                .PrimaryKey(t => t.intItemID);
            
            AddColumn("dbo.Division", "ysnActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Division", "ysnActive");
            DropTable("dbo.StoreItems");
            DropTable("dbo.RiderAddOns");
            DropTable("dbo.PlacePointValues");
            DropTable("dbo.Person");
            DropTable("dbo.HorseRiderScores");
            DropTable("dbo.HorseRider");
            DropTable("dbo.GlobalShowSettings");
            DropTable("dbo.DivisionClassMapping");
            DropTable("dbo.ClassRegistration");
            DropTable("dbo.ClassPlacing");
            DropTable("dbo.Class");
        }
    }
}
