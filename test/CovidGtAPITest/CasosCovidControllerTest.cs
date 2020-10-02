using System;
using System.Threading.Tasks;
using CovidGtAPI.Common;
using CovidGtAPI.Controllers;
using CovidGtAPI.Entities;
using CovidGtAPI.Services;
using Moq;
using Xunit;

namespace CovidGtAPITest
{
    public class CasosCovidControllerTest
    {
        
        public CasosCovidController controller;
        public Mock<ICasosCovidService> casosCovidServiceMock = new Mock<ICasosCovidService>();

        public CasosCovidControllerTest()
        {
            controller = new CasosCovidController(casosCovidServiceMock.Object);
        }

        [Fact]
        public async Task Insert_Ok()
        {
            
            long nextId = 1;
            var newCaso = new CasoCovid {
                Edad = 23,
                Sexo = "M",
                FechaRegistro = DateTime.Now,
                Localizacion = new Localizacion {
                    Pais = "Guatemala",
                    Departamento = "Guatemala",
                    Municipio = "Guatemala"
                }
            };

            casosCovidServiceMock.Setup(s => s.Insert(newCaso))
                .ReturnsAsync(nextId);

            long idNewCaso = (await controller.InsertCaso(newCaso)).Value;

            Assert.Equal(nextId, idNewCaso);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("X")]
        public async Task Insert_SexoInvalido(string sexo)
        {
            
            var newCaso = new CasoCovid {
                Edad = 23,
                Sexo = sexo,
                FechaRegistro = DateTime.Now,
                Localizacion = new Localizacion {
                    Pais = "Guatemala",
                    Departamento = "Guatemala",
                    Municipio = "Guatemala"
                }
            };
            casosCovidServiceMock.Setup(s => s.Insert(newCaso))
                .ReturnsAsync(1);

            await Assert.ThrowsAsync<ValidationException>(() => this.controller.InsertCaso(newCaso));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(121)]
        public async Task Insert_EdadInvalida(int edad)
        {
            
            var newCaso = new CasoCovid {
                Edad = edad,
                Sexo = "M",
                FechaRegistro = DateTime.Now,
                Localizacion = new Localizacion {
                    Pais = "Guatemala",
                    Departamento = "Guatemala",
                    Municipio = "Guatemala"
                }
            };
            casosCovidServiceMock.Setup(s => s.Insert(newCaso))
                .ReturnsAsync(1);

            await Assert.ThrowsAsync<ValidationException>(() => this.controller.InsertCaso(newCaso));
        }

    }
}