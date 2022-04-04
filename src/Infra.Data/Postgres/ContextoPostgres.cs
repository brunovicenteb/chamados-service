using Chamados.Service.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Chamados.Service.Infra.Data.Postgres;
public class ContextoPostgres : DbContext
{
    public ContextoPostgres()
    {
    }

    public ContextoPostgres(DbContextOptions<ContextoPostgres> opcoes)
        : base(opcoes)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    optionsBuilder.UseNpgsql("User ID=;Password=;Host=;Port=5432;Pooling=true;Connection Lifetime=0;");
    //}

    public DbSet<Chamado> Chamados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.Entity<Chamado>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Chamado>()
            .HasIndex(e => e.CPF)
            .IsUnique();
        modelBuilder.Entity<Chamado>(entity =>
        {
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Descricao).IsRequired();
            entity.Property(e => e.Assunto).IsRequired();
            entity.Property(e => e.CPF)
                .HasMaxLength(11)
                .IsRequired();
            entity.Property(e => e.Gravidade).IsRequired();
            entity.Property(e => e.NomePessoa)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Aberto).IsRequired();
            entity.Property(e => e.DataHoraCriacao)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.Property(e => e.DataHoraUltimaAtualizacao)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnUpdate();
        });
        base.OnModelCreating(modelBuilder);
    }
}