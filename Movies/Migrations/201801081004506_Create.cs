namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        customerId = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false, maxLength: 100),
                        lastName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.customerId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        movieId = c.Int(nullable: false, identity: true),
                        genre = c.String(nullable: false, maxLength: 100),
                        title = c.String(nullable: false, maxLength: 100),
                        length = c.Int(nullable: false),
                        numberOf = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.movieId);
            
            CreateTable(
                "dbo.MovieCustomers",
                c => new
                    {
                        Movie_movieId = c.Int(nullable: false),
                        Customer_customerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_movieId, t.Customer_customerId })
                .ForeignKey("dbo.Movies", t => t.Movie_movieId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_customerId, cascadeDelete: true)
                .Index(t => t.Movie_movieId)
                .Index(t => t.Customer_customerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieCustomers", "Customer_customerId", "dbo.Customers");
            DropForeignKey("dbo.MovieCustomers", "Movie_movieId", "dbo.Movies");
            DropIndex("dbo.MovieCustomers", new[] { "Customer_customerId" });
            DropIndex("dbo.MovieCustomers", new[] { "Movie_movieId" });
            DropTable("dbo.MovieCustomers");
            DropTable("dbo.Movies");
            DropTable("dbo.Customers");
        }
    }
}
