namespace MVCexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAvailableStockColumn : DbMigration
    {
        public override void Up()
        {
            Sql("update Movies set AvailableStock=10 where ID=1");
            Sql("update Movies set AvailableStock=12 where ID=2");
            Sql("update Movies set AvailableStock=15 where ID=3");
            Sql("update Movies set AvailableStock=7 where ID=4");
        }
        
        public override void Down()
        {
        }
    }
}
