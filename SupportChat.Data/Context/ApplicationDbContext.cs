using Microsoft.EntityFrameworkCore;
using SupportChat.Data.Implementation;
using SupportChat.Models;

namespace SupportChat.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<Seniority> Seniorities { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seniority Seed

            modelBuilder.Entity<Seniority>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });
            modelBuilder.Entity<Seniority>().HasData(new Seniority
            {
                Id = 1,
                Name = "Junior",
                SeniorityMultiplier = 0.4,
                MaxConcurrentChats = 5,
                TeamLeadResponsibilities = false,
                ShiftStartTime = new TimeSpan(9, 0, 0),
                ShiftEndTime = new TimeSpan(17, 0, 0),
            });

            modelBuilder.Entity<Seniority>().HasData(new Seniority
            {
                Id = 2,
                Name = "Mid-Level",
                SeniorityMultiplier = 0.6,
                MaxConcurrentChats = 10,
                TeamLeadResponsibilities = false,
                ShiftStartTime = new TimeSpan(9, 0, 0),
                ShiftEndTime = new TimeSpan(17, 0, 0),
            });

            modelBuilder.Entity<Seniority>().HasData(new Seniority
            {
                Id = 3,
                Name = "Senior",
                SeniorityMultiplier = 0.8,
                MaxConcurrentChats = 15,
                TeamLeadResponsibilities = true,
                ShiftStartTime = new TimeSpan(9, 0, 0),
                ShiftEndTime = new TimeSpan(17, 0, 0),
            });

            modelBuilder.Entity<Seniority>().HasData(new Seniority
            {
                Id = 4,
                Name = "Team Lead",
                SeniorityMultiplier = 0.5,
                MaxConcurrentChats = 20,
                TeamLeadResponsibilities = true,
                ShiftStartTime = new TimeSpan(9, 0, 0),
                ShiftEndTime = new TimeSpan(17, 0, 0),
            });

            #endregion Seniority Seed
        }
    }
}
