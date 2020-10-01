using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidGtAPI.Common;
using CovidGtAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CovidGtAPI.Services
{
    public class CasosCovidService : ICasosCovidService
    {
        
        private readonly ICovidGtDbContext context;

        public CasosCovidService(ICovidGtDbContext context)
        {
            this.context = context;

        }

        public async Task<List<CasoCovid>> GetAll(Filtro filtro)
        {
            var casos = this.context.CasosCovid.Where(c => true);
            if (filtro.Depto != null)
            {
                casos = casos.Where(c => c.Localizacion.Departamento == filtro.Depto);
            }
            if (filtro.Municipio != null)
            {
                casos = casos.Where(c => c.Localizacion.Municipio == filtro.Municipio);
            }
            if (filtro.Sexo != null)
            {
                casos = casos.Where(c => c.Sexo == filtro.Sexo);
            }
            if (filtro.Edad != null)
            {
                casos = casos.Where(c => c.Edad == filtro.Edad);
            }
            return await casos.ToListAsync();
        }

        public async Task<long> Insert(CasoCovid nuevoCaso)
        {
            await this.context.CasosCovid.AddAsync(nuevoCaso);
            await this.context.GuardarCambios();
            return nuevoCaso.Id;
        }

        public async Task Delete(long idCaso)
        {
            var entity = await this.context.CasosCovid
                .FirstOrDefaultAsync(c => c.Id == idCaso);
            if (entity == null)
            {
                throw new NotFoundException($"No existe un caso con ID='{idCaso}'");
            }
            this.context.CasosCovid.Remove(entity);
            await this.context.GuardarCambios();
        }

    }
}