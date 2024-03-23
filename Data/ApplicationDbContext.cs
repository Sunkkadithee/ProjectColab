using System;
using COMP2139_labs.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using COMP2139_labs.Areas.ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;


namespace COMP2139_labs.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding Projects
            builder.Entity<Project>().HasData(
            new Project
            {
                ProjectId = 1,
                Name = "COMP2139 Assignment 1",
            Description = "First COMP2139 Assignment",
                StartDate = new DateTime(2024, 1, 8),
                EndDate = new DateTime(2024, 2, 24),
                Status = "Closed"
            },
            new Project
            {
                ProjectId = 2,
                Name = "COMP2139 Assignment 2",
                Description = "Second COMP2139 Assignment",
                StartDate = new DateTime(2024, 3, 15),
                EndDate = new DateTime(2023, 4, 16),
                Status = "In Progress"

            });

            // Seeding ProjectTasks
            builder.Entity<ProjectTask>().HasData(

                new ProjectTask
                {
                    ProjectTaskId = 1,
                    Title = "Database Design",
                    Description = "Database Design for Assignment 1",
                    ProjectId = 1
                },

                new ProjectTask
                {
                    ProjectTaskId = 2,
                    Title = "Authentication",
                    Description = "Authenticaiton Using Identity Core",
                    ProjectId = 1

                }
               );
            builder.HasDefaultSchema("Identity");

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Role");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });


        }
    }
}




