using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Actividad1.Models
{
    public partial class sistem14_mapacurricularContext : DbContext
    {
        public sistem14_mapacurricularContext()
        {
        }

        public sistem14_mapacurricularContext(DbContextOptions<sistem14_mapacurricularContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carreras> Carreras { get; set; }
        public virtual DbSet<Materias> Materias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=204.93.167.23;user=sistem14_admin;password=admin2020;database=sistem14_mapacurricular", x => x.ServerVersion("5.6.46-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carreras>(entity =>
            {
                entity.ToTable("carreras");

                entity.HasIndex(e => e.Clave)
                    .HasName("Clave_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Especialidad)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Plan)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Materias>(entity =>
            {
                entity.ToTable("materias");

                entity.HasIndex(e => e.IdCarrera)
                    .HasName("fkmat_idx1");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnType("varchar(8)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Creditos).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.HorasPracticas).HasColumnType("tinyint(4)");

                entity.Property(e => e.HorasTeoricas).HasColumnType("tinyint(4)");

                entity.Property(e => e.IdCarrera).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(65)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Semestre).HasColumnType("tinyint(3) unsigned");

                entity.HasOne(d => d.IdCarreraNavigation)
                    .WithMany(p => p.Materias)
                    .HasForeignKey(d => d.IdCarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkmat");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
