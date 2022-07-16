using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class QuizContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Grade> Grades { get; set; }
        
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                .HasIndex(q => q.Slug)
                .IsUnique();
            
            // a question must only appear once per quiz
            modelBuilder.Entity<Question>()
                .HasIndex(q => new { q.QuestionText, q.QuizId })
                .IsUnique();

            // an answer must only appear once per multiple choice question
            modelBuilder.Entity<Answer>()
                .HasIndex(a => new { a.AnswerText, a.QuestionId })
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<QuestionType>()
                .HasIndex(qt => qt.Type)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}