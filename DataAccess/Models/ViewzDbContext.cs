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
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageDetails> PageDetails { get; set; }
        public virtual DbSet<PageHtmlContent> PageHtmlContent { get; set; }
        public virtual DbSet<PageMdContent> PageMdContent { get; set; }
        public virtual DbSet<Wiki> Wiki { get; set; }
        public virtual DbSet<WikiHtmlDescription> WikiHtmlDescription { get; set; }
        public virtual DbSet<WikiMdDescription> WikiMdDescription { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contents>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.Order })
                    .HasName("PK__tmp_ms_x__36A471A7161CF576");

                entity.ToTable("contents", "wiki");

                entity.HasIndex(e => new { e.PageId, e.Id })
                    .HasName("UQ__contents__B790C1F68FDA0895")
                    .IsUnique();

                entity.Property(e => e.PageId).HasColumnName("pageId");

                entity.Property(e => e.Order).HasColumnName("order");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasMaxLength(255);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contents__pageId__17036CC0");
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.HasKey(e => new { e.WikiId, e.ImageName })
                    .HasName("PK__images__00EE579B7814E494");

                entity.ToTable("images", "wiki");

                entity.Property(e => e.WikiId).HasColumnName("wikiId");

                entity.Property(e => e.ImageName)
                    .HasColumnName("imageName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image");

                entity.HasOne(d => d.Wiki)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.WikiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__images__wikiId__73BA3083");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("page", "wiki");

                entity.HasIndex(e => new { e.WikiId, e.Url })
                    .HasName("wikiPageUrl")
                    .IsUnique();

                entity.Property(e => e.PageId).HasColumnName("pageId");

                entity.Property(e => e.HitCount).HasColumnName("hitCount");

                entity.Property(e => e.PageName).HasColumnType("ntext");

                entity.Property(e => e.Url)
                    .IsRequired()
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
                entity.HasKey(e => new { e.PageId, e.Order })
                    .HasName("PK_WikiDetails");

                entity.ToTable("pageDetails", "wiki");

                entity.HasIndex(e => new { e.PageId, e.DetValue })
                    .HasName("UQ__tmp_ms_x__F25C5ED5CE0F7570")
                    .IsUnique();

                entity.Property(e => e.PageId).HasColumnName("pageId");

                entity.Property(e => e.Order).HasColumnName("order");

                entity.Property(e => e.DetKey)
                    .IsRequired()
                    .HasColumnName("detKey")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetValue)
                    .HasColumnName("detValue")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageDetails)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WikiPage");
            });

            modelBuilder.Entity<PageHtmlContent>(entity =>
            {
                entity.HasKey(e => e.PageId)
                    .HasName("PK__pageHtml__54B1FF747131F7EC");

                entity.ToTable("pageHtmlContent", "wiki");

                entity.Property(e => e.PageId)
                    .HasColumnName("pageId")
                    .ValueGeneratedNever();

                entity.Property(e => e.HtmlContent).HasColumnType("ntext");

                entity.HasOne(d => d.Page)
                    .WithOne(p => p.PageHtmlContent)
                    .HasForeignKey<PageHtmlContent>(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pageHtmlC__pageI__68487DD7");
            });

            modelBuilder.Entity<PageMdContent>(entity =>
            {
                entity.HasKey(e => e.PageId)
                    .HasName("PK__pageMdCo__54B1FF748DFBB833");

                entity.ToTable("pageMdContent", "wiki");

                entity.Property(e => e.PageId)
                    .HasColumnName("pageId")
                    .ValueGeneratedNever();

                entity.Property(e => e.MdContent).HasColumnType("ntext");

                entity.HasOne(d => d.Page)
                    .WithOne(p => p.PageMdContent)
                    .HasForeignKey<PageMdContent>(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pageMdCon__pageI__656C112C");
            });

            modelBuilder.Entity<Wiki>(entity =>
            {
                entity.ToTable("wiki", "wiki");

                entity.HasIndex(e => e.Url)
                    .HasName("UQ__wiki__C5B214314968C6DB")
                    .IsUnique();

                entity.Property(e => e.HitCount).HasColumnName("hitCount");

                entity.Property(e => e.PageName).HasColumnType("ntext");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WikiHtmlDescription>(entity =>
            {
                entity.HasKey(e => e.WikiId)
                    .HasName("PK__wikiHtml__54B1FF7462781F78");

                entity.ToTable("wikiHtmlDescription", "wiki");

                entity.Property(e => e.WikiId)
                    .HasColumnName("wikiId")
                    .ValueGeneratedNever();

                entity.Property(e => e.HtmlDescription).HasColumnType("ntext");

                entity.HasOne(d => d.Wiki)
                    .WithOne(p => p.WikiHtmlDescription)
                    .HasForeignKey<WikiHtmlDescription>(d => d.WikiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__wikiHtmlD__pageI__6E01572D");
            });

            modelBuilder.Entity<WikiMdDescription>(entity =>
            {
                entity.HasKey(e => e.WikiId)
                    .HasName("PK__wikiMdDe__54B1FF744921A12E");

                entity.ToTable("wikiMdDescription", "wiki");

                entity.Property(e => e.WikiId)
                    .HasColumnName("wikiId")
                    .ValueGeneratedNever();

                entity.Property(e => e.MdDescription).HasColumnType("ntext");

                entity.HasOne(d => d.Wiki)
                    .WithOne(p => p.WikiMdDescription)
                    .HasForeignKey<WikiMdDescription>(d => d.WikiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__wikiMdDes__pageI__6B24EA82");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
