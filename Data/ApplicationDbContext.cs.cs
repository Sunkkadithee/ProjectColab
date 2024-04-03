using COMP2139_labs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            /*
                        // Seeding Projects
                        builder.Entity<Project>().HasData(
                        new Project
                        {
                            ProjectId = 1,
                            Name = "COMP2139 Assignment 1",
                            Description = "First COMP2139 Assignment",
                            StartDate = new DateTime(1, 8,2002),
                            EndDate = new DateTime(2, 2, 2020),
                            Status = "Closed"
                        },
                        new Project
                        {
                            ProjectId = 2,
                            Name = "COMP2139 Assignment 2",
                            Description = "Second COMP2139 Assignment",
                            StartDate = new DateTime(3, 4,2023),
                            EndDate = new DateTime(2, 4, 2025),
                            Status = "In Progress"

                        });

                        // Seeding ProjectTasks
                        builder.Entity<ProjectTasks>().HasData(

                            new ProjectTasks
                            {
                                ProjectTaskId = 1,
                                Title = "Database Design",
                                Description = "Database Design for Assignment 1",
                                ProjectId = 1
                            },

                            new ProjectTasks
                            {
                                ProjectTaskId = 2,
                                Title = "Authentication",
                                Description = "Authenticaiton Using Identity Core",
                                ProjectId = 1

                            }
                           );*/
            builder.HasDefaultSchema("Identity");

            builder.Entity<IdentityUser>(entity =>
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

