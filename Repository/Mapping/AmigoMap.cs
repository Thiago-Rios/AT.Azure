using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping
{
    class AmigoMap : IEntityTypeConfiguration<Amigo>
    {
        public void Configure(EntityTypeBuilder<Amigo> builder)
        {
            builder.ToTable("Amigo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(200);

            builder.HasOne<Pessoa>(x => x.AmigoPessoa);
        }
    }
}
