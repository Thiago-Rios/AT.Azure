using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mapping
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {

        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Foto).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DataDeNascimento).IsRequired();

            builder.HasOne<Pais>(x => x.Pais);
            builder.HasOne<Estado>(x => x.Estado);
        }
    }
}
