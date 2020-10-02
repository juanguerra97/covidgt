using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CovidGtAPI;
using CovidGtAPI.Common;
using CovidGtAPI.Entities;
using CovidGtAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CovidGtAPITest
{
    public class CasosCovidServiceTest
    {

        private readonly CovidGtDbContext context;
        private readonly ICasosCovidService service;

        public CasosCovidServiceTest()
        {
            context = new CovidGtDbContext(new DbContextOptionsBuilder<CovidGtDbContext>()
                .UseInMemoryDatabase(databaseName: "covidTest")
                .Options);

            context.Database.EnsureDeleted();
            context.CasosCovid.AddRange(TestCollection());
            context.SaveChanges();

            service = new CasosCovidService(context);

        }

        [Fact]
        public async Task Delete_NotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => this.service.Delete(1000));
        }

        [Theory]
        [InlineData("Guatemala", 5)]
        [InlineData("Quetzaltenango", 2)]
        [InlineData("Chiquimula", 0)]
        public async Task GetAll_FiltroDepartamento(string depto, int count)
        {
            var casos = await this.service.GetAll(new Filtro
            {
                Depto = depto,
                Municipio = null,
                Edad = null,
                Sexo = null
            });
            Assert.Equal(count, casos.Count);
        }

        [Theory]
        [InlineData("Guatemala", 3)]
        [InlineData("Quetzaltenango", 1)]
        [InlineData("Salcaja", 1)]
        [InlineData("Villa Nueva", 2)]
        public async Task GetAll_FiltroMunicipio(string municipio, int count)
        {
            var casos = await this.service.GetAll(new Filtro
            {
                Depto = null,
                Municipio = municipio,
                Edad = null,
                Sexo = null
            });
            Assert.Equal(count, casos.Count);
        }

        [Theory]
        [InlineData(23, 2)]
        [InlineData(18, 1)]
        [InlineData(34, 1)]
        [InlineData(71, 1)]
        [InlineData(50, 0)]
        public async Task GetAll_FiltroEdad(int edad, int count)
        {
            var casos = await this.service.GetAll(new Filtro
            {
                Depto = null,
                Municipio = null,
                Edad = edad,
                Sexo = null
            });
            Assert.Equal(count, casos.Count);
        }

        [Theory]
        [InlineData("M", 5)]
        [InlineData("F", 2)]
        public async Task GetAll_FiltroSexo(string sexo, int count)
        {
            var casos = await this.service.GetAll(new Filtro
            {
                Depto = null,
                Municipio = null,
                Edad = null,
                Sexo = sexo
            });
            Assert.Equal(count, casos.Count);
        }

        public static List<CasoCovid> TestCollection()
        {
            return new List<CasoCovid> {
                new CasoCovid {
                    Id = 1,
                    Edad = 23,
                    Sexo = "M",
                    FechaRegistro = DateTime.Now,
                    Localizacion = new Localizacion {
                        Pais = "Guatemala",
                        Departamento = "Guatemala",
                        Municipio = "Guatemala"
                    }
                },
                new CasoCovid {
                    Id = 2,
                    Edad = 18,
                    Sexo = "M",
                    FechaRegistro = DateTime.Now,
                    Localizacion = new Localizacion {
                        Pais = "Guatemala",
                        Departamento = "Guatemala",
                        Municipio = "Guatemala"
                    }
                },
                new CasoCovid {
                    Id = 3,
                    Edad = 84,
                    Sexo = "F",
                    FechaRegistro = DateTime.Now,
                    Localizacion = new Localizacion {
                        Pais = "Guatemala",
                        Departamento = "Guatemala",
                        Municipio = "Guatemala"
                    }
                },
                new CasoCovid {
                    Id = 4,
                    Edad = 23,
                    Sexo = "F",
                    FechaRegistro = DateTime.Now,
                    Localizacion = new Localizacion {
                        Pais = "Guatemala",
                        Departamento = "Quetzaltenango",
                        Municipio = "Quetzaltenango"
                    }
                },
                new CasoCovid {
                    Id = 5,
                    Edad = 48,
                    Sexo = "M",
                    FechaRegistro = DateTime.Now,
                    Localizacion = new Localizacion {
                        Pais = "Guatemala",
                        Departamento = "Quetzaltenango",
                        Municipio = "Salcaja"
                    }
                },
                new CasoCovid {
                    Id = 6,
                    Edad = 71,
                    Sexo = "M",
                    FechaRegistro = DateTime.Now,
                    Localizacion = new Localizacion {
                        Pais = "Guatemala",
                        Departamento = "Guatemala",
                        Municipio = "Villa Nueva"
                    }
                },
                new CasoCovid {
                    Id = 7,
                    Edad = 34,
                    Sexo = "M",
                    FechaRegistro = DateTime.Now,
                    Localizacion = new Localizacion {
                        Pais = "Guatemala",
                        Departamento = "Guatemala",
                        Municipio = "Villa Nueva"
                    }
                },
            };
        }

    }
}