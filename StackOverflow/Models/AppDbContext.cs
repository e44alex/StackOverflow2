using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StackOverflow.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<Question>().HasIndex(u => u.Id).IsUnique();

            modelBuilder.Entity<Answer>().HasIndex(u => u.Id).IsUnique();

            modelBuilder.Entity<AnswerLiker>().HasOne(u => u.User).WithMany(x => x.LikedAnswers);
            modelBuilder.Entity<AnswerLiker>().HasOne(u => u.Answer).WithMany(x => x.Users);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(a => a.UserId);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(a => a.RoleId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(a => a.UserId);
        }
    }
}