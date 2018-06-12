namespace ProjekatAzil.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Dogs", "YearOfBirth");
            AddColumn("dbo.Dogs", "YearOfBirth", c => c.Int(nullable: false));
           
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dogs", "YearOfBirth");
            AddColumn("dbo.Dogs", "YearOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
