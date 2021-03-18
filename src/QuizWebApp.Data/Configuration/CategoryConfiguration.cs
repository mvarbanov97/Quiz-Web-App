using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizWebApp.Models;
using System;

namespace QuizWebApp.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Fluent API for the Category Entity
        /// <para>Will be executed in the OnModelCreating method of the ApplicationDbContext class</para>
        /// </summary>
        /// <param name="category">EFCore modelBuilder</param>
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category
                .HasMany(c => c.Questions)
                .WithOne(q => q.Category)
                .HasForeignKey(q => q.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            category
                .HasMany(c => c.Quizzes)
                .WithOne(q => q.Category)
                .HasForeignKey(q => q.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
