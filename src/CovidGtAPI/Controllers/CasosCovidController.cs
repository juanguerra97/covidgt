using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CovidGtAPI.Common;
using CovidGtAPI.Entities;
using CovidGtAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CovidGtAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CasosCovidController : ControllerBase
    {
        private readonly ICasosCovidService casosCovidService;

        public CasosCovidController(ICasosCovidService casosCovidService)
        {
            this.casosCovidService = casosCovidService;
        }

        public async Task<List<CasoCovid>> GetAll(string? depto = null, string? municipio = null, string? sexo = null, int? edad = null)
        {
            return await this.casosCovidService.GetAll(new Filtro
            {
                Depto = depto,
                Municipio = municipio,
                Sexo = sexo,
                Edad = edad
            });
        }

        [HttpPost]
        public async Task<ActionResult<long>> InsertCaso(CasoCovid datos)
        {
            if (datos.Sexo.Equals("M", StringComparison.InvariantCultureIgnoreCase)
                || datos.Sexo.Equals("F", StringComparison.InvariantCultureIgnoreCase))
            {
                datos.Sexo = datos.Sexo.ToUpper();
            } else {
                throw new ValidationException($"'{datos.Sexo}' no es un valor valido para sexo");
            }
            if (datos.Edad < 0 || datos.Edad > 120)
            {
                throw new ValidationException($"'{datos.Edad}' no es una edad valida");
            }
            return await this.casosCovidService.Insert(datos);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await this.casosCovidService.Delete(id);
            return NoContent();
        }

    }
}