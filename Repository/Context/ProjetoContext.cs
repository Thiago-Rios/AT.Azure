using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context
{
    public class ProjetoContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Amigo> Amigos { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(b => { b.AddConsole(); });

        public ProjetoContext(DbContextOptions<ProjetoContext> options) : base(options)
        {
                    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaMap());
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new AmigoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
