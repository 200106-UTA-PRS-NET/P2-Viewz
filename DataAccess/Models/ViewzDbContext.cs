using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class ViewzDBContext : DbContext
    {
        public ViewzDBContext()
        {
        }

        public ViewzDBContext(DbContextOptions<ViewzDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contents> Contents { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageDetails> PageDetails { get; set; }
        public virtual DbSet<Wiki> Wiki { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contents>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.Id })
                    .HasName("PK__tmp_ms_x__B790C1F771AE9A11");

                entity.ToTable("contents", "wiki");

                entity.Property(e => e.PageId).HasColumnName("pageId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(255);

                entity.Property(e => e.WikiId).HasColumnName("wikiId");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contents__pageId__5EBF139D");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("page", "wiki");

                entity.HasIndex(e => new { e.WikiId, e.Url })
                    .HasName("wikiPageUrl")
                    .IsUnique();

                entity.Property(e => e.PageId).HasColumnName("pageId");

                entity.Property(e => e.HtmlContent).HasColumnType("ntext");

                entity.Property(e => e.MdContent).HasColumnType("ntext");

                entity.Property(e => e.PageName).HasColumnType("ntext");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WikiId).HasColumnName("wikiId");

                entity.HasOne(d => d.Wiki)
                    .WithMany(p => p.Page)
                    .HasForeignKey(d => d.WikiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__page__wikiId__60A75C0F");
            });

            modelBuilder.Entity<PageDetails>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.DetKey })
                    .HasName("PK_WikiDetails");

                entity.ToTable("pageDetails", "wiki");

                entity.Property(e => e.PageId).HasColumnName("pageId");

                entity.Property(e => e.DetKey)
                    .HasColumnName("detKey")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetValue)
                    .HasColumnName("detValue")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WikiId).HasColumnName("wikiId");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageDetails)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WikiPage");
            });

            modelBuilder.Entity<Wiki>(entity =>
            {
                entity.ToTable("wiki", "wiki");

                entity.HasIndex(e => e.Url)
                    .HasName("UQ__wiki__C5B214316D7F0DFB")
                    .IsUnique();

                entity.Property(e => e.DescriptionHtml).HasColumnType("ntext");

                entity.Property(e => e.DescriptionMd).HasColumnType("ntext");

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
