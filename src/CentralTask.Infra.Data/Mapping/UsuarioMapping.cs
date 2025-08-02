using CentralTask.Domain.Entidades;
using CentralTask.Infra.Data.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralTask.Infra.Data.Mapping;

public class UsuarioMapping : EntidadeMapping<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.ToTable(ConstantesInfra.Tabelas.Usuario, ConstantesInfra.Schemas.Public);

        builder.Property(x => x.Nome)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Sobrenome)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(250)
            .IsRequired();
    }
}