using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizWebApp.Models;
using System;

namespace QuizWebApp.Data.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        /// <summary>
        /// Fluent API for the Category Entity
        /// <para>Will be executed in the OnModelCreating method of the ApplicationDbContext class</para>
        /// </summary>
        /// <param name="question">EFCore modelbuilder</param>
        public void Configure(EntityTypeBuilder<Question> question)
        {
            question
                .HasOne(q => q.Category)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
