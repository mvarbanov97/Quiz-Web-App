using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Score> Score { get; set; } 

        public DbSet<QuizQuestion> QuizQuestions { get; set; }

        public DbSet<QuizCategory> QuizCategories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            builder.Entity<User>().ToTable("Users", "dbo");
        }
    }
}
