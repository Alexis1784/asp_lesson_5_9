using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace asp_lesson_5_8.Models
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public StudentsContext()
            : base("DefaultConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .Map(t => t.MapLeftKey("CourseId")
                .MapRightKey("StudentId")
                .ToTable("CourseStudent"));
        }
    }
}