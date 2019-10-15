namespace MVCexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateType : DbMigration
    {
        public override void Up()
        {
            Sql("Insert MembershipTypes(Type, Duration, SignUpFee, Discount) Values ('Yearly',12,1200,20)");
            Sql("Insert MembershipTypes(Type, Duration, SignUpFee, Discount) Values ('Half-Yearly',6,600,15)");
            Sql("Insert MembershipTypes(Type, Duration, SignUpFee, Discount) Values ('Quarterly',3,300,10)");
            Sql("Insert MembershipTypes(Type, Duration, SignUpFee, Discount) Values ('PayAsYouGo',0,0,0)");
        }
        
        public override void Down()
        {
        }
    }
}
