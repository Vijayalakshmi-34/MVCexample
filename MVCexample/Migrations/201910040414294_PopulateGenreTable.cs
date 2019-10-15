namespace MVCexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert genres(Name)values('Crime Thriller')");
            Sql("Insert genres(Name)values('Comedy')");
            Sql("Insert genres(Name)values('Action')");
        }
        
        public override void Down()
        {
        }
    }
}
