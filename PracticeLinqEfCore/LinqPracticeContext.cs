using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PracticeLinqEfCore.Entity;
using Type = PracticeLinqEfCore.Entity.Type;

namespace PracticeLinqEfCore
{
    public partial class LinqPracticeContext : DbContext
    {
        public LinqPracticeContext()
        {
        }

        public LinqPracticeContext(DbContextOptions<LinqPracticeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Biere> Biere { get; set; }
        public virtual DbSet<Brasserie> Brasserie { get; set; }
        public virtual DbSet<Marque> Marque { get; set; }
        public virtual DbSet<Pays> Pays { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Biere>(entity =>
            {
                entity.HasKey(e => new { e.NomMarque, e.Version })
                    .HasName("pk_Biere");

                entity.Property(e => e.NomMarque)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Caracteristiques).HasMaxLength(500);

                entity.Property(e => e.CouleurBiere)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateCreation).HasColumnType("date");

                entity.HasOne(d => d.NomMarqueNavigation)
                    .WithMany(p => p.Biere)
                    .HasForeignKey(d => d.NomMarque)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_BiereMarque");

                entity.HasOne(d => d.NumTypeNavigation)
                    .WithMany(p => p.Biere)
                    .HasForeignKey(d => d.NumType)
                    .HasConstraintName("FK_BIERE_TYPE");
            });

            modelBuilder.Entity<Brasserie>(entity =>
            {
                entity.HasKey(e => e.CodeBrasserie)
                    .HasName("PK_BRASSERIE");

                entity.Property(e => e.CodeBrasserie)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomBrasserie).HasMaxLength(45);

                entity.Property(e => e.NomRegion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ville).HasMaxLength(50);

                entity.HasOne(d => d.NomRegionNavigation)
                    .WithMany(p => p.Brasserie)
                    .HasForeignKey(d => d.NomRegion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BRASSERIE_REGION");
            });

            modelBuilder.Entity<Marque>(entity =>
            {
                entity.HasKey(e => e.NomMarque)
                    .HasName("pk_NomMarque");

                entity.Property(e => e.NomMarque)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodeBrasserie)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeBrasserieNavigation)
                    .WithMany(p => p.Marque)
                    .HasForeignKey(d => d.CodeBrasserie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MARQUE_BRASSERIE");
            });

            modelBuilder.Entity<Pays>(entity =>
            {
                entity.HasKey(e => e.NomPays)
                    .HasName("PK_PAYS");

                entity.Property(e => e.NomPays).HasMaxLength(20);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.NomRegion)
                    .HasName("PK_REGION");

                entity.Property(e => e.NomRegion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomPays)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.NomPaysNavigation)
                    .WithMany(p => p.Region)
                    .HasForeignKey(d => d.NomPays)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REGION_PAYS");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.NroType)
                    .HasName("PK_TYPE");

                entity.Property(e => e.NroType).ValueGeneratedNever();

                entity.Property(e => e.Commentaire).HasMaxLength(80);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Fermentation).HasMaxLength(10);

                entity.Property(e => e.NomType).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
