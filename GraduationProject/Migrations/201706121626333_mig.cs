namespace GraduationProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complaints", "Answer", c => c.String());
            DropColumn("dbo.Complaints", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Complaints", "Role", c => c.String());
            DropColumn("dbo.Complaints", "Answer");
        }
    }
}
