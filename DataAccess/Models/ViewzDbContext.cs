using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class ViewzDbContext : DbContext
    {
        public ViewzDbContext()
        {
        }

        public ViewzDbContext(DbContextOptions<ViewzDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageDetails> PageDetails { get; set; }
        public virtual DbSet<Wiki> Wiki { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(e => new { e.WikiId, e.PageId })
                    .HasName("PK_WikiPage");

                entity.ToTable("page", "wiki");

                entity.HasIndex(e => new { e.WikiId, e.Url })
                    .HasName("wikiPageUrl")
                    .IsUnique();

                entity.Property(e => e.WikiId).HasColumnName("wikiId");

                entity.Property(e => e.PageId)
                    .HasColumnName("pageId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("ntext");

                entity.Property(e => e.HtmlContent)
                    .HasColumnName("HTMLContent")
                    .HasColumnType("ntext");

                entity.Property(e => e.PageName).HasColumnType("ntext");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Wiki)
                    .WithMany(p => p.Page)
                    .HasForeignKey(d => d.WikiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__page__wikiId__4F7CD00D");
            });

            modelBuilder.Entity<PageDetails>(entity =>
            {
                entity.HasKey(e => new { e.WikiId, e.PageId, e.DetKey })
                    .HasName("PK_WikiDetails");

                entity.ToTable("pageDetails", "wiki");

                entity.Property(e => e.WikiId).HasColumnName("wikiId");

                entity.Property(e => e.PageId).HasColumnName("pageId");

                entity.Property(e => e.DetKey)
                    .HasColumnName("detKey")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetValue)
                    .HasColumnName("detValue")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageDetails)
                    .HasForeignKey(d => new { d.WikiId, d.PageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WikiPage");
            });

            modelBuilder.Entity<Wiki>(entity =>
            {
                entity.ToTable("wiki", "wiki");

                entity.HasIndex(e => e.Url)
                    .HasName("UQ__wiki__C5B214316D7F0DFB")
                    .IsUnique();

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.PageName).HasColumnType("ntext");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
