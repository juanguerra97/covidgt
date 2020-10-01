using CovidGtAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CovidGtAPI
{
    public class CovidGtDbContext : DbContext, ICovidGtDbContext
    {
        public CovidGtDbContext(DbContextOptions options)
        :base(options)
        { }

        public DbSet<CasoCovid> CasosCovid { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CasoCovid>(casoCovidBuilder => {

                casoCovidBuilder.Property(c => c.Id)
                    .ValueGeneratedOnAdd();
                
                casoCovidBuilder.Property(c => c.Sexo)
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .IsRequired(true);

                casoCovidBuilder.Property(c => c.FechaRegistro)
                    .IsRequired(true);

                casoCovidBuilder.Property(c => c.Edad).IsRequired(true);

                casoCovidBuilder.OwnsOne(c => c.Localizacion, b => {
                    b.Property(l => l.Pais)
                        .HasMaxLength(Localizacion.MAX_PAIS_LENGTH)
                        .IsRequired();
                    b.Property(l => l.Departamento)
                        .HasMaxLength(Localizacion.MAX_DEPARTAMENTO_LENGTH);
                    b.Property(l => l.Municipio)
                        .HasMaxLength(Localizacion.MAX_MUNICIPIO_LENGTH)
                        .IsRequired();
                    b.HasIndex(l => l.Pais).IsUnique(false);
                    b.HasIndex(l => l.Departamento).IsUnique(false);
                    b.HasIndex(l => l.Municipio).IsUnique(false);
                });

            });
        }

    }
}