
using CentralTask.Domain.Entidades;
using CentralTask.Infra.Data.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralTask.Infra.Data.Mapping
{
    public class UsersMapping : EntidadeMapping<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            base.Configure(builder);
            builder.ToTable(ConstantesInfra.Tabelas.Users, ConstantesInfra.Schemas.Public);
            builder.HasKey(e => e.Id);
            
        }
    }
}