namespace SDQWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Zips", t => t.ZipCode)
                .Index(t => t.CountryId)
                .Index(t => t.ZipCode);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PresentAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryID = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryID)
                .ForeignKey("dbo.Zips", t => t.ZipCode)
                .Index(t => t.CountryID)
                .Index(t => t.ZipCode);
            
            CreateTable(
                "dbo.Institutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TypeId = c.Int(nullable: false),
                        WebSite = c.String(),
                        EstDate = c.DateTime(nullable: false),
                        ContactId = c.Int(nullable: false),
                        NumberOfDepertment = c.Byte(nullable: false),
                        PresentAddressId = c.Int(),
                        AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.PresentAddresses", t => t.PresentAddressId)
                .Index(t => t.PresentAddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Depertments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressId = c.Int(nullable: false),
                        InstituteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Institutes", t => t.InstituteId)
                .Index(t => t.AddressId)
                .Index(t => t.InstituteId);
            
            CreateTable(
                "dbo.Coursses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cradit = c.Byte(nullable: false),
                        Description = c.String(),
                        DepertmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Depertments", t => t.DepertmentId)
                .Index(t => t.DepertmentId);
            
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(),
                        Details = c.String(),
                        Date = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Coursse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id)
                .Index(t => t.Coursse_Id);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(),
                        Date = c.DateTime(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Coursse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id)
                .Index(t => t.Coursse_Id);
            
            CreateTable(
                "dbo.ExamSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        CourseId = c.Int(),
                        Coursse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id)
                .Index(t => t.Coursse_Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(),
                        Date = c.DateTime(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Coursse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id)
                .Index(t => t.Coursse_Id);
            
            CreateTable(
                "dbo.Routines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                        Time = c.Time(nullable: false, precision: 7),
                        CourseId = c.Int(nullable: false),
                        Room = c.Int(nullable: false),
                        Coursse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id)
                .Index(t => t.Coursse_Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        Notes = c.String(),
                        CourseId = c.Int(nullable: false),
                        Coursse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id)
                .Index(t => t.Coursse_Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Video = c.String(),
                        MaterialsId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        QuiseId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Coursse_Id = c.Int(nullable: false),
                        Coursse_Id1 = c.Int(),
                        Material_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id1)
                .ForeignKey("dbo.Materials", t => t.Material_ID)
                .Index(t => t.Coursse_Id1)
                .Index(t => t.Material_ID);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LectureSlide = c.String(),
                        VideoLink = c.String(),
                        PresentationFile = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SessionQuises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(),
                        QuiseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quises", t => t.QuiseId)
                .ForeignKey("dbo.Sessions", t => t.SessionId)
                .Index(t => t.SessionId)
                .Index(t => t.QuiseId);
            
            CreateTable(
                "dbo.Quises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Coursse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.Coursse_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Started = c.DateTime(nullable: false),
                        Graduated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StudentDepertments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepertmentId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Depertments", t => t.DepertmentId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.DepertmentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        MobileNo = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.Byte(nullable: false),
                        Password = c.String(),
                        PresentAddressId = c.Int(),
                        AddressID = c.Int(),
                        Profile = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.AddressID)
                .ForeignKey("dbo.PresentAddresses", t => t.PresentAddressId)
                .Index(t => t.PresentAddressId)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        UserId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        Descriptions = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Joined = c.DateTime(nullable: false),
                        Resign = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        Coursse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coursses", t => t.Coursse_Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.Coursse_Id);
            
            CreateTable(
                "dbo.TeacherDepertments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        DepertmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Depertments", t => t.DepertmentId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.DepertmentId);
            
            CreateTable(
                "dbo.UserInstitutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        InstituteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institutes", t => t.InstituteId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.InstituteId);
            
            CreateTable(
                "dbo.Zips",
                c => new
                    {
                        ZipCode = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ZipCode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PresentAddresses", "ZipCode", "dbo.Zips");
            DropForeignKey("dbo.Addresses", "ZipCode", "dbo.Zips");
            DropForeignKey("dbo.Institutes", "PresentAddressId", "dbo.PresentAddresses");
            DropForeignKey("dbo.Depertments", "InstituteId", "dbo.Institutes");
            DropForeignKey("dbo.UserInstitutes", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserInstitutes", "InstituteId", "dbo.Institutes");
            DropForeignKey("dbo.Teachers", "UserId", "dbo.Users");
            DropForeignKey("dbo.TeacherDepertments", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherDepertments", "DepertmentId", "dbo.Depertments");
            DropForeignKey("dbo.TeacherCourses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherCourses", "Coursse_Id", "dbo.Coursses");
            DropForeignKey("dbo.Students", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "PresentAddressId", "dbo.PresentAddresses");
            DropForeignKey("dbo.Notes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Discussions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Discussions", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Users", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.StudentDepertments", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentDepertments", "DepertmentId", "dbo.Depertments");
            DropForeignKey("dbo.StudentCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "Coursse_Id", "dbo.Coursses");
            DropForeignKey("dbo.SessionQuises", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.SessionQuises", "QuiseId", "dbo.Quises");
            DropForeignKey("dbo.Sessions", "Material_ID", "dbo.Materials");
            DropForeignKey("dbo.Sessions", "Coursse_Id1", "dbo.Coursses");
            DropForeignKey("dbo.Schedules", "Coursse_Id", "dbo.Coursses");
            DropForeignKey("dbo.Routines", "Coursse_Id", "dbo.Coursses");
            DropForeignKey("dbo.Reports", "Coursse_Id", "dbo.Coursses");
            DropForeignKey("dbo.ExamSchedules", "Coursse_Id", "dbo.Coursses");
            DropForeignKey("dbo.Coursses", "DepertmentId", "dbo.Depertments");
            DropForeignKey("dbo.Assignments", "Coursse_Id", "dbo.Coursses");
            DropForeignKey("dbo.Announcements", "Coursse_Id", "dbo.Coursses");
            DropForeignKey("dbo.Depertments", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Institutes", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.PresentAddresses", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "CountryId", "dbo.Countries");
            DropIndex("dbo.UserInstitutes", new[] { "InstituteId" });
            DropIndex("dbo.UserInstitutes", new[] { "UserId" });
            DropIndex("dbo.TeacherDepertments", new[] { "DepertmentId" });
            DropIndex("dbo.TeacherDepertments", new[] { "TeacherId" });
            DropIndex("dbo.TeacherCourses", new[] { "Coursse_Id" });
            DropIndex("dbo.TeacherCourses", new[] { "TeacherId" });
            DropIndex("dbo.Teachers", new[] { "UserId" });
            DropIndex("dbo.Notes", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Discussions", new[] { "PostId" });
            DropIndex("dbo.Discussions", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "AddressID" });
            DropIndex("dbo.Users", new[] { "PresentAddressId" });
            DropIndex("dbo.StudentDepertments", new[] { "StudentId" });
            DropIndex("dbo.StudentDepertments", new[] { "DepertmentId" });
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.StudentCourses", new[] { "Coursse_Id" });
            DropIndex("dbo.StudentCourses", new[] { "StudentId" });
            DropIndex("dbo.SessionQuises", new[] { "QuiseId" });
            DropIndex("dbo.SessionQuises", new[] { "SessionId" });
            DropIndex("dbo.Sessions", new[] { "Material_ID" });
            DropIndex("dbo.Sessions", new[] { "Coursse_Id1" });
            DropIndex("dbo.Schedules", new[] { "Coursse_Id" });
            DropIndex("dbo.Routines", new[] { "Coursse_Id" });
            DropIndex("dbo.Reports", new[] { "Coursse_Id" });
            DropIndex("dbo.ExamSchedules", new[] { "Coursse_Id" });
            DropIndex("dbo.Assignments", new[] { "Coursse_Id" });
            DropIndex("dbo.Announcements", new[] { "Coursse_Id" });
            DropIndex("dbo.Coursses", new[] { "DepertmentId" });
            DropIndex("dbo.Depertments", new[] { "InstituteId" });
            DropIndex("dbo.Depertments", new[] { "AddressId" });
            DropIndex("dbo.Institutes", new[] { "AddressId" });
            DropIndex("dbo.Institutes", new[] { "PresentAddressId" });
            DropIndex("dbo.PresentAddresses", new[] { "ZipCode" });
            DropIndex("dbo.PresentAddresses", new[] { "CountryID" });
            DropIndex("dbo.Addresses", new[] { "ZipCode" });
            DropIndex("dbo.Addresses", new[] { "CountryId" });
            DropTable("dbo.Zips");
            DropTable("dbo.UserInstitutes");
            DropTable("dbo.TeacherDepertments");
            DropTable("dbo.TeacherCourses");
            DropTable("dbo.Teachers");
            DropTable("dbo.Notes");
            DropTable("dbo.Posts");
            DropTable("dbo.Discussions");
            DropTable("dbo.Users");
            DropTable("dbo.StudentDepertments");
            DropTable("dbo.Students");
            DropTable("dbo.StudentCourses");
            DropTable("dbo.Quises");
            DropTable("dbo.SessionQuises");
            DropTable("dbo.Materials");
            DropTable("dbo.Sessions");
            DropTable("dbo.Schedules");
            DropTable("dbo.Routines");
            DropTable("dbo.Reports");
            DropTable("dbo.ExamSchedules");
            DropTable("dbo.Assignments");
            DropTable("dbo.Announcements");
            DropTable("dbo.Coursses");
            DropTable("dbo.Depertments");
            DropTable("dbo.Institutes");
            DropTable("dbo.PresentAddresses");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
        }
    }
}
