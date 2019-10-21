using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Models
{
    public partial class GufosContext : DbContext
    {
        public GufosContext()
        {
        }

        public GufosContext(DbContextOptions<GufosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Localizacao> Localizacao { get; set; }
        public virtual DbSet<Presenca> Presenca { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=N-1S-DEV-04\\SQLEXPRESS; Database=Gufos; User Id=sa; Password=132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Categori__7B406B56CC020AA1")
                    .IsUnique();

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.Property(e => e.AcessoLivre).HasDefaultValueSql("((1))");

                entity.Property(e => e.Titulo).IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK__Evento__Categori__4F7CD00D");

                entity.HasOne(d => d.Localizacao)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.LocalizacaoId)
                    .HasConstraintName("FK__Evento__Localiza__5165187F");
            });

            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.HasIndex(e => e.Cnpj)
                    .HasName("UQ__Localiza__AA57D6B4A64C90CB")
                    .IsUnique();

                entity.HasIndex(e => e.RazaoSocial)
                    .HasName("UQ__Localiza__7DD028762CAD44F3")
                    .IsUnique();

                entity.Property(e => e.Cnpj)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Endereco).IsUnicode(false);

                entity.Property(e => e.RazaoSocial).IsUnicode(false);
            });

            modelBuilder.Entity<Presenca>(entity =>
            {
                entity.Property(e => e.PresencaStatus).IsUnicode(false);

                entity.HasOne(d => d.Evento)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.EventoId)
                    .HasConstraintName("FK__Presenca__Evento__5441852A");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Presenca__Usuari__5535A963");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Tipo_Usu__7B406B560A0B6B47")
                    .IsUnique();

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.HasOne(d => d.TipoUsuario)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.TipoUsuarioId)
                    .HasConstraintName("FK__Usuario__Tipo_Us__5629CD9C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
