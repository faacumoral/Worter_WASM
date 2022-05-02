using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Worter.DAO.Models
{
    public partial class WorterContext : DbContext
    {
        public WorterContext()
        {
        }

        public WorterContext(DbContextOptions<WorterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Translation> Translation { get; set; }
        public virtual DbSet<Word> Word { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.IdLanguage)
                    .HasName("PK__Language__1656D9172765D4AC");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent)
                    .HasName("PK__Student__61B351045882A987");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Translation>(entity =>
            {
                entity.HasKey(e => e.IdTranslation)
                    .HasName("PK__Translat__4DAFC6B3905B84ED");

                entity.Property(e => e.SearchValue).IsUnicode(false);

                entity.Property(e => e.Translate).IsUnicode(false);

                entity.HasOne(d => d.IdWordNavigation)
                    .WithMany(p => p.Translation)
                    .HasForeignKey(d => d.IdWord)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Translation_Word");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.HasKey(e => e.IdWord)
                    .HasName("PK__Word__2E9FC62CBEC06D11");

                entity.Property(e => e.Meaning).IsUnicode(false);

                entity.Property(e => e.SearchValue).IsUnicode(false);

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany(p => p.Word)
                    .HasForeignKey(d => d.IdLanguage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Language");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Word)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Word_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
