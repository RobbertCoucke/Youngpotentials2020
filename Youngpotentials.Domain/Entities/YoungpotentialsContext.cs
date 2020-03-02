using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Youngpotentials.Domain.Entities
{
    public partial class YoungpotentialsContext : DbContext
    {
        public YoungpotentialsContext()
        {
        }

        public YoungpotentialsContext(DbContextOptions<YoungpotentialsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Afstudeerrichting> Afstudeerrichting { get; set; }
        public virtual DbSet<AfstudeerrichtingOffer> AfstudeerrichtingOffer { get; set; }
        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<Keuze> Keuze { get; set; }
        public virtual DbSet<KeuzeOffer> KeuzeOffer { get; set; }
        public virtual DbSet<Offers> Offers { get; set; }
        public virtual DbSet<Opleiding> Opleiding { get; set; }
        public virtual DbSet<OpleidingOffer> OpleidingOffer { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Studiegebied> Studiegebied { get; set; }
        public virtual DbSet<StudiegebiedOffer> StudiegebiedOffer { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=youngpotentials.database.windows.net;Database=Youngpotentials;Trusted_Connection=False;Encrypt=True;;User ID=beheerder;Password=Vives2020*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Afstudeerrichting>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AfstudeerrichtingNaam)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.OpleidingId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Opleiding)
                    .WithMany(p => p.Afstudeerrichting)
                    .HasForeignKey(d => d.OpleidingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Afstudeerrichting_Opleiding");
            });

            modelBuilder.Entity<AfstudeerrichtingOffer>(entity =>
            {
                entity.HasKey(e => new { e.IdAfstudeerrichting, e.IdOffer });

                entity.ToTable("Afstudeerrichting_Offer");

                entity.Property(e => e.IdAfstudeerrichting)
                    .HasColumnName("idAfstudeerrichting")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdOffer).HasColumnName("idOffer");

                entity.HasOne(d => d.IdAfstudeerrichtingNavigation)
                    .WithMany(p => p.AfstudeerrichtingOffer)
                    .HasForeignKey(d => d.IdAfstudeerrichting)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Afstudeerrichting_Offer_Afstudeerrichting");

                entity.HasOne(d => d.IdOfferNavigation)
                    .WithMany(p => p.AfstudeerrichtingOffer)
                    .HasForeignKey(d => d.IdOffer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Afstudeerrichting_Offer_Offers");
            });

            modelBuilder.Entity<Applications>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OfferId).HasColumnName("OfferID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applications_Offers");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applications_Students");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Telephone).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetUsers_AspNetRoles");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyName).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Url).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Companies_AspNetUsers");
            });

            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.HasIndex(e => new { e.OfferId, e.StudentId })
                    .HasName("IX_FK_OfferStudentOffer");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OfferStudents_Offers");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OfferStudents_Students");
            });

            modelBuilder.Entity<Keuze>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AfstudeerrichtingId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Keuze1)
                    .IsRequired()
                    .HasColumnName("Keuze")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.Afstudeerrichting)
                    .WithMany(p => p.Keuze)
                    .HasForeignKey(d => d.AfstudeerrichtingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Keuze_Afstudeerrichting");
            });

            modelBuilder.Entity<KeuzeOffer>(entity =>
            {
                entity.HasKey(e => new { e.IdKeuze, e.IdOffer });

                entity.ToTable("Keuze_Offer");

                entity.Property(e => e.IdKeuze)
                    .HasColumnName("idKeuze")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdOffer).HasColumnName("idOffer");

                entity.HasOne(d => d.IdKeuzeNavigation)
                    .WithMany(p => p.KeuzeOffer)
                    .HasForeignKey(d => d.IdKeuze)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Keuze_Offer_Keuze");

                entity.HasOne(d => d.IdOfferNavigation)
                    .WithMany(p => p.KeuzeOffer)
                    .HasForeignKey(d => d.IdOffer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Keuze_Offer_Offers");
            });

            modelBuilder.Entity<Offers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Companies_Offers");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Type_Offers");
            });

            modelBuilder.Entity<Opleiding>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdStudiegebied)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NaamOpleiding)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsFixedLength();

                entity.HasOne(d => d.IdStudiegebiedNavigation)
                    .WithMany(p => p.Opleiding)
                    .HasForeignKey(d => d.IdStudiegebied)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opleiding_Studiegebied");
            });

            modelBuilder.Entity<OpleidingOffer>(entity =>
            {
                entity.HasKey(e => new { e.IdOpleiding, e.IdOffer })
                    .HasName("PK_Opleiding_Offer_1");

                entity.ToTable("Opleiding_Offer");

                entity.Property(e => e.IdOpleiding)
                    .HasColumnName("idOpleiding")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdOffer).HasColumnName("idOffer");

                entity.HasOne(d => d.IdOfferNavigation)
                    .WithMany(p => p.OpleidingOffer)
                    .HasForeignKey(d => d.IdOffer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opleiding_Offer_Offers");

                entity.HasOne(d => d.IdOpleidingNavigation)
                    .WithMany(p => p.OpleidingOffer)
                    .HasForeignKey(d => d.IdOpleiding)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opleiding_Offer_Opleiding");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Students_AspNetUsers");
            });

            modelBuilder.Entity<Studiegebied>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsGraduate).HasColumnName("isGraduate");

                entity.Property(e => e.Kleur)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Studiegebied1)
                    .IsRequired()
                    .HasColumnName("Studiegebied")
                    .HasMaxLength(60)
                    .IsFixedLength();
            });

            modelBuilder.Entity<StudiegebiedOffer>(entity =>
            {
                entity.HasKey(e => new { e.IdStudiegebied, e.IdOffer })
                    .HasName("PK_Studiegebied_Offer_1");

                entity.ToTable("Studiegebied_Offer");

                entity.Property(e => e.IdStudiegebied)
                    .HasColumnName("idStudiegebied")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdOffer).HasColumnName("idOffer");

                entity.HasOne(d => d.IdOfferNavigation)
                    .WithMany(p => p.StudiegebiedOffer)
                    .HasForeignKey(d => d.IdOffer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Studiegebied_Offer_Offers");

                entity.HasOne(d => d.IdStudiegebiedNavigation)
                    .WithMany(p => p.StudiegebiedOffer)
                    .HasForeignKey(d => d.IdStudiegebied)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Studiegebied_Offer_Studiegebied_Offer");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
