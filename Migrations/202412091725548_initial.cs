namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActorAddFormViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AlternateName = c.String(),
                        BirthDate = c.DateTime(),
                        Height = c.Double(),
                        ImageUrl = c.String(),
                        Biography = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActorBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AlternateName = c.String(),
                        BirthDate = c.DateTime(),
                        Height = c.Double(),
                        ImageUrl = c.String(maxLength: 250),
                        Executive = c.String(),
                        Biography = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActorMediaItemBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ActorWithShowInfoViewModel_Id = c.Int(),
                        ActorWithShowInfoViewModel_Id1 = c.Int(),
                        ActorWithShowInfoViewModel_Id2 = c.Int(),
                        ActorWithShowInfoViewModel_Id3 = c.Int(),
                        ActorWithShowInfoViewModel_Id4 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActorBaseViewModels", t => t.ActorWithShowInfoViewModel_Id)
                .ForeignKey("dbo.ActorBaseViewModels", t => t.ActorWithShowInfoViewModel_Id1)
                .ForeignKey("dbo.ActorBaseViewModels", t => t.ActorWithShowInfoViewModel_Id2)
                .ForeignKey("dbo.ActorBaseViewModels", t => t.ActorWithShowInfoViewModel_Id3)
                .ForeignKey("dbo.ActorBaseViewModels", t => t.ActorWithShowInfoViewModel_Id4)
                .Index(t => t.ActorWithShowInfoViewModel_Id)
                .Index(t => t.ActorWithShowInfoViewModel_Id1)
                .Index(t => t.ActorWithShowInfoViewModel_Id2)
                .Index(t => t.ActorWithShowInfoViewModel_Id3)
                .Index(t => t.ActorWithShowInfoViewModel_Id4);
            
            CreateTable(
                "dbo.ActorMediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        Content = c.Binary(nullable: false),
                        Caption = c.String(nullable: false, maxLength: 250),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.ActorId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        AlternateName = c.String(maxLength: 150),
                        BirthDate = c.DateTime(),
                        Height = c.Double(),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Executive = c.String(nullable: false, maxLength: 250),
                        Biography = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Genre = c.String(nullable: false, maxLength: 50),
                        ReleaseDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Coordinator = c.String(nullable: false, maxLength: 250),
                        Premise = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        SeasonNumber = c.Int(nullable: false),
                        EpisodeNumber = c.Int(nullable: false),
                        Genre = c.String(nullable: false),
                        AirDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Clerk = c.String(nullable: false, maxLength: 250),
                        Premise = c.String(),
                        ShowId = c.Int(nullable: false),
                        VideoContentType = c.String(maxLength: 100),
                        Video = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.ShowId)
                .Index(t => t.ShowId);
            
            CreateTable(
                "dbo.EpisodeBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        SeasonNumber = c.Int(nullable: false),
                        EpisodeNumber = c.Int(nullable: false),
                        Genre = c.String(nullable: false),
                        AirDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Clerk = c.String(),
                        ShowId = c.Int(nullable: false),
                        Premise = c.String(),
                        VideoContentType = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Show_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShowBaseViewModels", t => t.Show_Id)
                .Index(t => t.Show_Id);
            
            CreateTable(
                "dbo.ShowBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Genre = c.String(nullable: false, maxLength: 50),
                        ReleaseDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Coordinator = c.String(),
                        Premise = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ShowActors",
                c => new
                    {
                        Show_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Show_Id, t.Actor_Id })
                .ForeignKey("dbo.Shows", t => t.Show_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Show_Id)
                .Index(t => t.Actor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EpisodeBaseViewModels", "Show_Id", "dbo.ShowBaseViewModels");
            DropForeignKey("dbo.Episodes", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.ShowActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.ShowActors", "Show_Id", "dbo.Shows");
            DropForeignKey("dbo.ActorMediaItems", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.ActorMediaItemBaseViewModels", "ActorWithShowInfoViewModel_Id4", "dbo.ActorBaseViewModels");
            DropForeignKey("dbo.ActorMediaItemBaseViewModels", "ActorWithShowInfoViewModel_Id3", "dbo.ActorBaseViewModels");
            DropForeignKey("dbo.ActorMediaItemBaseViewModels", "ActorWithShowInfoViewModel_Id2", "dbo.ActorBaseViewModels");
            DropForeignKey("dbo.ActorMediaItemBaseViewModels", "ActorWithShowInfoViewModel_Id1", "dbo.ActorBaseViewModels");
            DropForeignKey("dbo.ActorMediaItemBaseViewModels", "ActorWithShowInfoViewModel_Id", "dbo.ActorBaseViewModels");
            DropIndex("dbo.ShowActors", new[] { "Actor_Id" });
            DropIndex("dbo.ShowActors", new[] { "Show_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EpisodeBaseViewModels", new[] { "Show_Id" });
            DropIndex("dbo.Episodes", new[] { "ShowId" });
            DropIndex("dbo.ActorMediaItems", new[] { "ActorId" });
            DropIndex("dbo.ActorMediaItemBaseViewModels", new[] { "ActorWithShowInfoViewModel_Id4" });
            DropIndex("dbo.ActorMediaItemBaseViewModels", new[] { "ActorWithShowInfoViewModel_Id3" });
            DropIndex("dbo.ActorMediaItemBaseViewModels", new[] { "ActorWithShowInfoViewModel_Id2" });
            DropIndex("dbo.ActorMediaItemBaseViewModels", new[] { "ActorWithShowInfoViewModel_Id1" });
            DropIndex("dbo.ActorMediaItemBaseViewModels", new[] { "ActorWithShowInfoViewModel_Id" });
            DropTable("dbo.ShowActors");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RoleClaims");
            DropTable("dbo.Genres");
            DropTable("dbo.GenreBaseViewModels");
            DropTable("dbo.ShowBaseViewModels");
            DropTable("dbo.EpisodeBaseViewModels");
            DropTable("dbo.Episodes");
            DropTable("dbo.Shows");
            DropTable("dbo.Actors");
            DropTable("dbo.ActorMediaItems");
            DropTable("dbo.ActorMediaItemBaseViewModels");
            DropTable("dbo.ActorBaseViewModels");
            DropTable("dbo.ActorAddFormViewModels");
        }
    }
}
