using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApp.Data.Configuration
{
    public class QuizCategoryConfiguration : IEntityTypeConfiguration<QuizCategory>
    {
        public void Configure(EntityTypeBuilder<QuizCategory> quizCategory)
        {
            quizCategory
                .HasKey(k => new { k.QuizId, k.CategoryId});

            quizCategory
                .HasOne(qc => qc.Quiz)
                .WithMany(q => q.Categories)
                .HasForeignKey(qc => qc.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            quizCategory
                .HasOne(qc => qc.Category)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(qc => qc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
