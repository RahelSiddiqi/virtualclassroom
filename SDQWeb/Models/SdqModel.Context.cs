﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDQWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SDQEntities : DbContext
    {
        public SDQEntities()
            : base("name=SDQEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Coursse> Coursses { get; set; }
        public DbSet<Depertment> Depertments { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PresentAddress> PresentAddresses { get; set; }
        public DbSet<Quise> Quises { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionQuise> SessionQuises { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentDepertment> StudentDepertments { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<TeacherDepertment> TeacherDepertments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInstitute> UserInstitutes { get; set; }
        public DbSet<Zip> Zips { get; set; }
    }
}